using Contracts;
using Repository.Repositories;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<ICityRepository> _cityRepository;
    private readonly Lazy<IAdvertPurposeRepository> _advertPurposeRepository;
    private readonly Lazy<IAdvertTypeRepository> _advertTypeRepository;
    private readonly Lazy<INeighborhoodRepository> _neighborhoodRepository;
    private readonly Lazy<IAdvertRepository> _advertRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(repositoryContext));
        _advertPurposeRepository = new Lazy<IAdvertPurposeRepository>(() => new AdvertPurposeRepository(repositoryContext));
        _advertTypeRepository = new Lazy<IAdvertTypeRepository>(() => new AdvertTypeRepository(repositoryContext));
        _neighborhoodRepository = new Lazy<INeighborhoodRepository>(() => new NeighborhoodRepository(repositoryContext));
        _advertRepository = new Lazy<IAdvertRepository>(() => new AdvertRepository(repositoryContext));
    }

    public ICityRepository City => _cityRepository.Value;
    public IAdvertPurposeRepository AdvertPurpose => _advertPurposeRepository.Value;
    public IAdvertTypeRepository AdvertType => _advertTypeRepository.Value;
    public INeighborhoodRepository Neighborhood => _neighborhoodRepository.Value;
    public IAdvertRepository Advert => _advertRepository.Value;

}

