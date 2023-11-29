using KeyNekretnine.Application.Core.Auth.Commands.GoogleLogin;

namespace KeyNekretnine.Application.Abstraction.Authentication;
public interface IGoogleService
{
    Task<GoogleResponse> VerifyGoogleTokenAndGetInformations(string idToken);
}
