using Entities.Models;
using Google.Apis.Auth;
using Shared.DataTransferObjects.Auth;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;

namespace Service.Contracts;
public interface ITokenService
{
    Task<string> CreateAccessToken(User user);
    Task<string> CreateRefreshToken(User user);
    Task<TokenRequest> VerifyRefreshToken(TokenRequest request);
    Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(GoogleLoginDto googleLoginDto);
    Task<FBUserInfoDto> VerifyFacebookTokenAndGetUserInfo(string fbAccessToken);
};