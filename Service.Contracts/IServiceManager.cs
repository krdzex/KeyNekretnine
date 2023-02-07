namespace Service.Contracts;
public interface IServiceManager
{
    IAuthenticationService AuthenticationService { get; }
    ITokenService TokenService { get; }
    IImageService ImageService { get; }
}
