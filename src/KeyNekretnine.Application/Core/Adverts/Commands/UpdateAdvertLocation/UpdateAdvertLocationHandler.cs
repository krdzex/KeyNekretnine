using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using KeyNekretnine.Domain.Agents;
using Newtonsoft.Json;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertLocation;
internal sealed class UpdateAdvertLocationHandler : ICommandHandler<UpdateAdvertLocationCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAgentRepository _agentRepository;
    private readonly IUserContext _userContext;
    private readonly IAdvertUpdateRepository _advertUpdateRepository;


    public UpdateAdvertLocationHandler(
        IAdvertRepository advertRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IAgentRepository agentRepository,
        IUserContext userContext,
        IAdvertUpdateRepository advertUpdateRepository)
    {
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _agentRepository = agentRepository;
        _userContext = userContext;
        _advertUpdateRepository = advertUpdateRepository;
    }

    public async Task<Result> Handle(UpdateAdvertLocationCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetByReferenceIdAsync(request.ReferenceId, cancellationToken);

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

        var canUserAddUpdate = await _advertUpdateRepository.CanAddUpdate(advert.Id, UpdateTypes.Location);

        if (!canUserAddUpdate)
        {
            return Result.Failure(AdvertErrors.LocationcUpdateAlredyExist);
        }

        var oldContent = new UpdateAdvertLocationRequest(
            advert.Location.Latitude, advert.Location.Longitude,
            advert.Location.Address, advert.NeighborhoodId);

        var updateAdvert = AdvertUpdate.Create(
            advert.Id,
            UpdateTypes.Location,
            _dateTimeProvider.Now,
            JsonConvert.SerializeObject(request.LocationUpdateRequest),
            JsonConvert.SerializeObject(oldContent));

        _advertUpdateRepository.Add(updateAdvert);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}