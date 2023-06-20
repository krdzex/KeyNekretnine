namespace Contracts;
public interface IRepositoryManager
{
    ICityRepository City { get; }
    IAdvertPurposeRepository AdvertPurpose { get; }
    IAdvertTypeRepository AdvertType { get; }
    INeighborhoodRepository Neighborhood { get; }
    IAdvertRepository Advert { get; }
    IUserRepository User { get; }
    IImageRepository Image { get; }
    ITemporeryImageDataRepository TemporeryImageData { get; }
    IRejectReasonRepository RejectReason { get; }
    IAdvertFeatureRepository AdvertFeature { get; }
    IAdvertStatusRepository AdvertStatus { get; }
    IAgencyRepository Agency { get; }
    IPhoneNumberRepository PhoneNumber { get; }
}