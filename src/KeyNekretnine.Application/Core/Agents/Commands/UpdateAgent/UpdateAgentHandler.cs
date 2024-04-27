using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.ValueObjects;
using MediatR;

namespace KeyNekretnine.Application.Core.Agents.Commands.UpdateAgent;
internal sealed class UpdateAgentHandler : ICommandHandler<UpdateAgentCommand>
{
    private readonly IAgentRepository _agentRepository;
    private readonly IImageService _imageService;
    private readonly IImageToDeleteRepository _imageToDeleteRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    public UpdateAgentHandler(
        IAgentRepository agentRepository,
        IImageService imageService,
        IImageToDeleteRepository imageToDeleteRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _agentRepository = agentRepository;
        _imageService = imageService;
        _imageToDeleteRepository = imageToDeleteRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(UpdateAgentCommand request, CancellationToken cancellationToken)
    {
        var agent = await _agentRepository.GetByIdWithAgencyAndLanguagesAsync(request.AgentId, cancellationToken);

        if (agent is null)
        {
            return Result.Failure<Unit>(AgentErrors.NotFound);
        }

        if (agent.Agency.UserId != _userContext.UserId)
        {
            return Result.Failure<Unit>(AgencyErrors.NotOwner);
        }

        agent.Update(
            AgentFirstName.Create(request.FirstName ?? agent.FirstName.Value)!,
            AgentLastName.Create(request.LastName ?? agent.LastName.Value),
            AgentPhoneNumber.Create(request.PhoneNumber ?? agent.PhoneNumber.Value)!,
            Description.Create(request.Description ?? agent.Description?.Value)!,
            AgentEmail.Create(request.Email ?? agent.Email.Value)!,
            SocialMedia.Create(
                request.Twitter ?? agent.SocialMedia?.Twitter,
                request.Facebook ?? agent.SocialMedia?.Facebook,
                request.Instagram ?? agent.SocialMedia?.Instagram,
                request.Linkedin ?? agent.SocialMedia?.Linkedin),
            request.LanguageIds);

        if (request.Image?.Length > 0)
        {
            var oldImageUrl = agent.ImageUrl;
            var cloudinaryImgUrl = await _imageService.UploadImageOnCloudinary(request.Image);

            if (oldImageUrl is not null)
            {
                _imageToDeleteRepository.Add(oldImageUrl.Value, _dateTimeProvider.Now);
            }

            var imageUrl = ImageUrl.Create(cloudinaryImgUrl);

            agent.UpdateImage(imageUrl.Value);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
