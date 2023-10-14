using KeyNekretnine.Application.Core.Auth.Commands.RefreshTokens;
using KeyNekretnine.Domain.Users;

namespace KeyNekretnine.Application.Abstraction.Authentication;
public interface IJwtService
{
    Task<string> CreateAccessToken(User user);
    Task<string> CreateRefreshToken(User user);
    Task<RefreshTokenResponse> VerifyRefreshToken(string AccessToken, string RefreshToken);
    //Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(GoogleLoginDto googleLoginDto);
    //Task<FBUserInfoDto> VerifyFacebookTokenAndGetUserInfo(string fbAccessToken);
}
