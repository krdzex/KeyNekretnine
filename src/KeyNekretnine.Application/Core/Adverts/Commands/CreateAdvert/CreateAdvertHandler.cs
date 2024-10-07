
using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.TemporeryImageDatas;
using KeyNekretnine.Domain.ValueObjects;

namespace KeyNekretnine.Application.Core.Adverts.Commands.CreateAdvert;
internal sealed class CreateAdvertHandler : ICommandHandler<CreateAdvertCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAdvertRepository _advertRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserContext _userContext;
    private readonly ITemporeryImageDataRepository _temporeryImageDataRepository;

    public CreateAdvertHandler(
        IUnitOfWork unitOfWork,
        IAdvertRepository advertRepository,
        IDateTimeProvider dateTimeProvider,
        IUserContext userContext,
        ITemporeryImageDataRepository temporeryImageDataRepository)
    {
        _unitOfWork = unitOfWork;
        _advertRepository = advertRepository;
        _dateTimeProvider = dateTimeProvider;
        _userContext = userContext;
        _temporeryImageDataRepository = temporeryImageDataRepository;
    }

    public async Task<Result> Handle(CreateAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = Advert.Create(
            request.AdvertForCreating.Price,
            AdvertDescription.Create(
                request.AdvertForCreating.DescriptionSr,
                request.AdvertForCreating.DescriptionEn),
            request.AdvertForCreating.NoOfBedrooms,
            request.AdvertForCreating.FloorSpace,
            request.AdvertForCreating.NoOfBathrooms,
            request.AdvertForCreating.HasTerrace,
            request.AdvertForCreating.HasGarage,
            request.AdvertForCreating.IsFurnished,
            request.AdvertForCreating.HasWifi,
            request.AdvertForCreating.HasElevator,
            request.AdvertForCreating.BuildingFloor,
            request.AdvertForCreating.IsUrgent,
            request.AdvertForCreating.IsUnderConstruction,
            false,
            request.AdvertForCreating.YearOfBuildingCreated,
            AdvertStatus.Uploading,
            (AdvertPurpose)request.AdvertForCreating.Purpose,
            (AdvertType)request.AdvertForCreating.Type,
            request.AdvertForCreating.NeighborhoodId,
            Location.Create(
                request.AdvertForCreating.Address,
                request.AdvertForCreating.Latitude,
                request.AdvertForCreating.Longitude),
            _dateTimeProvider.Now,
            _userContext.AgencyId is null ? _userContext.UserId : null,
            _userContext.AgencyId is not null ? request.AdvertForCreating.AgentId : null);

        advert.AddFeatures(request.AdvertForCreating.Features);

        var coverImage = await _temporeryImageDataRepository.GetById(request.AdvertForCreating.CoverImageUrl, cancellationToken);

        if (coverImage is not null)
        {
            coverImage.AdvertId = advert.Id;
            coverImage.IsCover = true;
        }

        _advertRepository.Add(advert);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _temporeryImageDataRepository.BulkUpdateForCreatingAdvert(request.AdvertForCreating.ImageIds, advert.Id, cancellationToken);

        return Result.Success();
    }
}