namespace Contracts;

public interface IRepositoryManager
{
    ICityRepository City { get; }
    IAdvertPurposeRepository AdvertPurpose { get; }
    IAdvertTypeRepository AdvertType { get; }
    INeighborhoodRepository Neighborhood { get; }
    IAdvertRepository Advert { get; }
    IUserRepository User { get; }
}