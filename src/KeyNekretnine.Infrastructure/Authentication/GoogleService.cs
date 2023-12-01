using Google.Apis.Auth;
using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Core.Auth.Commands.GoogleLogin;

namespace KeyNekretnine.Infrastructure.Authentication;
internal sealed class GoogleService : IGoogleService
{
    public GoogleService()
    {
    }

    public async Task<GoogleResponse> VerifyGoogleTokenAndGetInformations(string idToken)
    {
        try
        {
            var googleClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { googleClientId }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var response = new GoogleResponse
            {
                Email = payload.Email,
                FirstName = payload.GivenName,
                LastName = payload.FamilyName,
                Subject = payload.Subject
            };

            return response;
        }
        catch
        {
            throw new UnauthorizedAccessException("Invalid token");
        }
    }
}
