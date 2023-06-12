using AutoMapper;
using Entities.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects.Auth;
using Shared.DataTransferObjects.User;

namespace Service;
internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AuthenticationService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
    {
        var user = _mapper.Map<User>(userForRegistration);
        var result = await _userManager.CreateAsync(user, userForRegistration.Password);

        if (!result.Succeeded)
        {
            return result;
        }

        var addRoleRsult = await _userManager.AddToRoleAsync(user, "User");

        if (!addRoleRsult.Succeeded)
        {
            return result;
        }

        return result;
    }

    public async Task<User> ValidateUser(UserForAuthenticationDto userForAuthentication)
    {
        var user = await _userManager.FindByEmailAsync(userForAuthentication.Email);

        if (user != null && await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
        {
            return user;
        }

        return null;
    }

    public async Task<User> GoogleLogin(GoogleLoginDto googleLoginDto, GoogleJsonWebSignature.Payload payload)
    {
        var info = new UserLoginInfo(googleLoginDto.Provider, payload.Subject, googleLoginDto.Provider);
        var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new User { Email = payload.Email, UserName = payload.Email, FirstName = payload.GivenName, LastName = payload.FamilyName, EmailConfirmed = true };
                await _userManager.CreateAsync(user);

                await _userManager.AddToRoleAsync(user, "User");
                await _userManager.AddLoginAsync(user, info);
            }
            else
            {
                await _userManager.AddLoginAsync(user, info);
            }
        }

        return user;
    }

    public async Task<User> FacebookLogin(FBUserInfoDto fbUserInfo)
    {
        var info = new UserLoginInfo("FACEBOOK", fbUserInfo.Id, "FACEBOOK");
        var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(fbUserInfo.Email);
            if (user == null)
            {
                user = new User { Email = fbUserInfo.Email, UserName = fbUserInfo.Email, FirstName = fbUserInfo.FirstName, LastName = fbUserInfo.LastName, EmailConfirmed = true };
                await _userManager.CreateAsync(user);

                await _userManager.AddToRoleAsync(user, "User");
                await _userManager.AddLoginAsync(user, info);
            }
            else
            {
                await _userManager.AddLoginAsync(user, info);
            }
        }

        return user;
    }
}