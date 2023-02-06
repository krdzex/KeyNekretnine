using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects.User;

namespace Service;
internal sealed class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserInformationDto> GetCurrentUserInformations(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var userInformations = _mapper.Map<UserInformationDto>(user);

        return userInformations;
    }
}
