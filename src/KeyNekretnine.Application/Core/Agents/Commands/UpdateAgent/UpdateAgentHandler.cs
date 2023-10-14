using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.Shared;
using MediatR;

namespace KeyNekretnine.Application.Core.Agents.Commands.UpdateAgent;
internal sealed class UpdateAgentHandler : ICommandHandler<UpdateAgentCommand>
{
    private readonly IAgentRepository _agentRepository;
    private readonly IImageService _imageService;
    private readonly IImageToDeleteRepository _imageToDeleteRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAgentHandler(
        IAgentRepository agentRepository,
        IImageService imageService,
        IImageToDeleteRepository imageToDeleteRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _agentRepository = agentRepository;
        _imageService = imageService;
        _imageToDeleteRepository = imageToDeleteRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateAgentCommand request, CancellationToken cancellationToken)
    {
        var agent = await _agentRepository.GetByIdWithAgencyAndLanguagesAsync(request.AgentId, cancellationToken);

        if (agent is null)
        {
            return Result.Failure<Unit>(AgentErrors.NotFound);
        }

        if (agent.Agency.UserId != request.UserId)
        {
            return Result.Failure<Unit>(AgencyErrors.NotOwner);
        }

        agent.Update(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new PhoneNumber(request.PhoneNumber),
            new Description(request.Description),
            new Email(request.Email),
            new SocialMedia(
                request.TwitterUrl,
                request.FacebookUrl,
                request.InstagramUrl,
                request.LinkedinUrl),
            request.LanguageIds);

        if (request.Image?.Length > 0)
        {
            var oldImageUrl = agent.ImageUrl;
            var imageUrl = await _imageService.UploadImageOnCloudinary(request.Image);

            if (oldImageUrl is not null)
            {
                _imageToDeleteRepository.Add(oldImageUrl.Value, _dateTimeProvider.Now);
            }
            agent.UpdateImage(new ImageUrl(imageUrl));
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
