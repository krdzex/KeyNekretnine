using AutoMapper;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects.Auth;

namespace Service;
internal class AuthenticationService : IAuthenticationService
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
            var errors = new Dictionary<string, string> { };
            foreach (var error in result.Errors)
            {
                errors.Add(error.Code, error.Description);
            }
            throw new AuthenticationException(errors);
        }

        await _userManager.AddToRoleAsync(user, "User");

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
}
