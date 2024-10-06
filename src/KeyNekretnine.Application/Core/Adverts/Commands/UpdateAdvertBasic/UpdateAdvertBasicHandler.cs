using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using KeyNekretnine.Domain.Agents;
using Newtonsoft.Json;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertBasic;

internal sealed class UpdateAdvertBasicHandler : ICommandHandler<UpdateAdvertBasicCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAgentRepository _agentRepository;
    private readonly IAdvertUpdateRepository _advertUpdateRepository;
    private readonly IUserContext _userContext;

    public UpdateAdvertBasicHandler(
        IAdvertRepository advertRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork,
        IAgentRepository agentRepository,
        IAdvertUpdateRepository advertUpdateRepository,
        IUserContext userContext)
    {
        _advertRepository = advertRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _agentRepository = agentRepository;
        _advertUpdateRepository = advertUpdateRepository;
        _userContext = userContext;
    }

    public async Task<Result> Handle(UpdateAdvertBasicCommand request, CancellationToken cancellationToken)
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

        var canUserAddUpdate = await _advertUpdateRepository.CanAddUpdate(advert.Id, UpdateTypes.BasicInformations);

        if (!canUserAddUpdate)
        {
            return Result.Failure(AdvertErrors.BasicUpdateAlredyExist);
        }

        var oldContent = new UpdateAdvertBasicRequest(
            advert.Description.Sr, advert.Description.En, advert.Price,
            advert.FloorSpace, advert.NoOfBedrooms, advert.NoOfBathrooms,
            (int)advert.Type, (int)advert.Purpose, advert.YearOfBuildingCreated, advert.BuildingFloor,
            advert.HasGarage, advert.IsFurnished, advert.HasWifi, advert.HasElevator,
            advert.IsUrgent, advert.HasTerrace, advert.IsUnderConstruction);

        var updateAdvert = AdvertUpdate.Create(
            advert.Id,
            UpdateTypes.BasicInformations,
            _dateTimeProvider.Now,
            JsonConvert.SerializeObject(request.BasicUpdateData),
            JsonConvert.SerializeObject(oldContent));

        _advertUpdateRepository.Add(updateAdvert);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}