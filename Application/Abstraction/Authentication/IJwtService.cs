using KeyNekretnine.Domain.Users;

namespace KeyNekretnine.Application.Abstraction.Authentication;
public interface IJwtService
{
    Task<string> CreateAccessToken(User user);
    Task<string> CreateRefreshToken(User user);
    //Task<TokenRequest> VerifyRefreshToken(TokenRequest request);
    //Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(GoogleLoginDto googleLoginDto);
    //Task<FBUserInfoDto> VerifyFacebookTokenAndGetUserInfo(string fbAccessToken);
}
