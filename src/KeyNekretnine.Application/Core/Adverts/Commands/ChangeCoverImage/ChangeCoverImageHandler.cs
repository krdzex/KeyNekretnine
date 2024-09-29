using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Agents;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ChangeCoverImage;
internal sealed class ChangeCoverImageHandler : ICommandHandler<ChangeCoverImageCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAgencyRepository _agencyRepository;
    private readonly IUserContext _userContext;
    private readonly IAgentRepository _agentRepository;

    public ChangeCoverImageHandler(
        IAdvertRepository advertRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IAgencyRepository agencyRepository,
        IUserContext userContext,
        IAgentRepository agentRepository)
    {
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _userContext = userContext;
        _agencyRepository = agencyRepository;
        _agentRepository = agentRepository;
    }

    public async Task<Result> Handle(ChangeCoverImageCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetByReferenceIdWithImagesAsync(request.ReferenceId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure(AdvertErrors.NotFound);
        }

        var canUserEditResult = await advert.CanCurrentUserUpdate(
            _userContext.AgencyId is not null,
            _userContext.UserId,
            _agentRepository);

        if (canUserEditResult.IsFailure)
        {
            return canUserEditResult;
        }

        var result = advert.ChangeCoverImage(request.NewCoverUrl, _dateTimeProvider.Now);

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}