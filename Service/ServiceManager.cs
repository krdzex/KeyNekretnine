using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service;
public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthenticationService> _authenticationService;
    private readonly Lazy<ITokenService> _tokenService;
    private readonly Lazy<IImageService> _imageService;
    private readonly Lazy<IEmailService> _emailService;

    public ServiceManager(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, mapper));
        _tokenService = new Lazy<ITokenService>(() => new TokenService(userManager, configuration));
        _imageService = new Lazy<IImageService>(() => new ImageService());
        _emailService = new Lazy<IEmailService>(() => new EmailService(configuration));
    }
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
    public ITokenService TokenService => _tokenService.Value;
    public IImageService ImageService => _imageService.Value;
    public IEmailService EmailService => _emailService.Value;
}
