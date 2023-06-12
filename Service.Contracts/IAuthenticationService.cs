using Entities.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects.Auth;
using Shared.DataTransferObjects.User;

namespace Service.Contracts;
public interface IAuthenticationService
{
    Task<User> ValidateUser(UserForAuthenticationDto userForAuthentication);
    Task<Tuple<string, IdentityResult>> RegisterUser(UserForRegistrationDto userForRegistration);
    Task<User> GoogleLogin(GoogleLoginDto googleLoginDto, GoogleJsonWebSignature.Payload payload);
    Task<User> FacebookLogin(FBUserInfoDto fbUserInfo);

}