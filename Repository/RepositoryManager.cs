﻿using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories;
using Service.Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<ICityRepository> _cityRepository;
    private readonly Lazy<IAdvertPurposeRepository> _advertPurposeRepository;
    private readonly Lazy<IAdvertTypeRepository> _advertTypeRepository;
    private readonly Lazy<INeighborhoodRepository> _neighborhoodRepository;
    private readonly Lazy<IAdvertRepository> _advertRepository;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IImageRepository> _imageRepository;
    private readonly DapperContext _dapperContext;
    private readonly Lazy<ITemporeryImageDataRepository> _temporeryImageDataRepository;


    public RepositoryManager(
        RepositoryContext repositoryContext,
        UserManager<User> userManager,
        DapperContext dapperContext,
        IServiceManager serviceManager,
        IProcessingChannel processingChannel
        )
    {
        _repositoryContext = repositoryContext;
        _dapperContext = dapperContext;
        _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(dapperContext));
        _advertPurposeRepository = new Lazy<IAdvertPurposeRepository>(() => new AdvertPurposeRepository(dapperContext));
        _advertTypeRepository = new Lazy<IAdvertTypeRepository>(() => new AdvertTypeRepository(dapperContext));
        _neighborhoodRepository = new Lazy<INeighborhoodRepository>(() => new NeighborhoodRepository(dapperContext));
        _advertRepository = new Lazy<IAdvertRepository>(() => new AdvertRepository(dapperContext, serviceManager, processingChannel));
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(dapperContext, userManager));
        _imageRepository = new Lazy<IImageRepository>(() => new ImageRepository(dapperContext));
        _temporeryImageDataRepository = new Lazy<ITemporeryImageDataRepository>(() => new TemporeryImageDataRepository(dapperContext));
    }

    public ICityRepository City => _cityRepository.Value;
    public IAdvertPurposeRepository AdvertPurpose => _advertPurposeRepository.Value;
    public IAdvertTypeRepository AdvertType => _advertTypeRepository.Value;
    public INeighborhoodRepository Neighborhood => _neighborhoodRepository.Value;
    public IAdvertRepository Advert => _advertRepository.Value;
    public IUserRepository User => _userRepository.Value;
    public IImageRepository Image => _imageRepository.Value;
    public ITemporeryImageDataRepository TemporeryImageData => _temporeryImageDataRepository.Value;

}

