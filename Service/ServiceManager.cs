﻿using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service;
public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthenticationService> _authenticationService;
    private readonly Lazy<ITokenService> _tokenService;
    private readonly Lazy<IUserService> _userService;

    public ServiceManager(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationService(userManager, mapper));
        _tokenService = new Lazy<ITokenService>(() =>
            new TokenService(userManager, configuration));
        _userService = new Lazy<IUserService>(() =>
            new UserService(userManager, mapper));
    }
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
    public ITokenService TokenService => _tokenService.Value;
    public IUserService UserService => _userService.Value;
}
