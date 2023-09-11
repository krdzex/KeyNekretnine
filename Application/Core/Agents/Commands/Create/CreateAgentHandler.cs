using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.Shared;
using MediatR;

namespace KeyNekretnine.Application.Core.Agents.Commands.Create;
internal sealed class CreateAgentHandler : ICommandHandler<CreateAgentCommand>
{
    private readonly IAgencyRepository _agencyRepository;
    private readonly IImageService _imageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAgentRepository _agentRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    public CreateAgentHandler(
        IAgencyRepository agencyRepository,
        IImageService imageService,
        IUnitOfWork unitOfWork,
        IAgentRepository agentRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _agencyRepository = agencyRepository;
        _imageService = imageService;
        _unitOfWork = unitOfWork;
        _agentRepository = agentRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
    {
        var agency = await _agencyRepository.GetByIdAsync(request.AgencyId, cancellationToken);

        if (agency is null)
        {
            return Result.Failure<Unit>(AgencyErrors.NotFound);
        }

        if (agency.UserId != request.UserId)
        {
            return Result.Failure<Unit>(AgencyErrors.NotOwner);

        }

        var agent = Agent.Create(
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
            agency.Id,
            request.LanguageIds,
            _dateTimeProvider.Now
            );

        if (request.Image?.Length > 0)
        {
            var imageUrl = await _imageService.UploadImageOnCloudinary(request.Image);

            agent.UpdateImage(new ImageUrl(imageUrl));
        }

        _agentRepository.Add(agent);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}