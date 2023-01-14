using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects.Auth;

namespace Service.Contracts;
public interface IAuthenticationService
{
    Task<User> ValidateUser(UserForAuthenticationDto userForAuthentication);
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
}

