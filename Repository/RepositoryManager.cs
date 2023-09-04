//using AutoMapper;
//using Contracts;
//using Entities.Models;
//using Microsoft.AspNetCore.Identity;
//using Repository.Repositories;

//namespace Repository;

//public sealed class RepositoryManager : IRepositoryManager
//{
//    private readonly Lazy<ICityRepository> _cityRepository;
//    private readonly Lazy<IAdvertPurposeRepository> _advertPurposeRepository;
//    private readonly Lazy<IAdvertTypeRepository> _advertTypeRepository;
//    private readonly Lazy<INeighborhoodRepository> _neighborhoodRepository;
//    private readonly Lazy<IAdvertRepository> _advertRepository;
//    private readonly Lazy<IUserRepository> _userRepository;
//    private readonly Lazy<IImageRepository> _imageRepository;
//    private readonly Lazy<ITemporeryImageDataRepository> _temporeryImageDataRepository;
//    private readonly Lazy<IRejectReasonRepository> _rejectReasonRepository;
//    private readonly Lazy<IAdvertFeatureRepository> _advertFeatureRepository;
//    private readonly Lazy<IAdvertStatusRepository> _advertStatusRepository;
//    private readonly Lazy<IAgencyRepository> _agencyRepository;
//    private readonly Lazy<IPhoneNumberRepository> _phoneNumberRepository;
//    private readonly Lazy<ILanguageRepository> _languageRepository;
//    private readonly Lazy<IAgentRepository> _agentRepository;

//    public RepositoryManager(IMapper mapper, UserManager<User> userManager, DapperContext dapperContext)
//    {
//        _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(dapperContext));
//        _advertPurposeRepository = new Lazy<IAdvertPurposeRepository>(() => new AdvertPurposeRepository(dapperContext));
//        _advertTypeRepository = new Lazy<IAdvertTypeRepository>(() => new AdvertTypeRepository(dapperContext));
//        _neighborhoodRepository = new Lazy<INeighborhoodRepository>(() => new NeighborhoodRepository(dapperContext));
//        _advertRepository = new Lazy<IAdvertRepository>(() => new AdvertRepository(dapperContext));
//        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(dapperContext, userManager, mapper));
//        _imageRepository = new Lazy<IImageRepository>(() => new ImageRepository(dapperContext));
//        _temporeryImageDataRepository = new Lazy<ITemporeryImageDataRepository>(() => new TemporeryImageDataRepository(dapperContext));
//        _rejectReasonRepository = new Lazy<IRejectReasonRepository>(() => new RejectReasonRepository(dapperContext));
//        _advertFeatureRepository = new Lazy<IAdvertFeatureRepository>(() => new AdvertFeatureRepository(dapperContext));
//        _advertStatusRepository = new Lazy<IAdvertStatusRepository>(() => new AdvertStatusRepository(dapperContext));
//        _agencyRepository = new Lazy<IAgencyRepository>(() => new AgencyRepository(dapperContext));
//        _phoneNumberRepository = new Lazy<IPhoneNumberRepository>(() => new PhoneNumberRepository(dapperContext));
//        _languageRepository = new Lazy<ILanguageRepository>(() => new LanguageRepository(dapperContext));
//        _agentRepository = new Lazy<IAgentRepository>(() => new AgentRepository(dapperContext));
//    }

//    public ICityRepository City => _cityRepository.Value;
//    public IAdvertPurposeRepository AdvertPurpose => _advertPurposeRepository.Value;
//    public IAdvertTypeRepository AdvertType => _advertTypeRepository.Value;
//    public INeighborhoodRepository Neighborhood => _neighborhoodRepository.Value;
//    public IAdvertRepository Advert => _advertRepository.Value;
//    public IUserRepository User => _userRepository.Value;
//    public IImageRepository Image => _imageRepository.Value;
//    public ITemporeryImageDataRepository TemporeryImageData => _temporeryImageDataRepository.Value;
//    public IRejectReasonRepository RejectReason => _rejectReasonRepository.Value;
//    public IAdvertFeatureRepository AdvertFeature => _advertFeatureRepository.Value;
//    public IAdvertStatusRepository AdvertStatus => _advertStatusRepository.Value;
//    public IAgencyRepository Agency => _agencyRepository.Value;
//    public IPhoneNumberRepository PhoneNumber => _phoneNumberRepository.Value;
//    public ILanguageRepository Language => _languageRepository.Value;
//    public IAgentRepository Agent => _agentRepository.Value;
//}