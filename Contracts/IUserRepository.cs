using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;

namespace Contracts;
public interface IUserRepository
{
    Task UserBanExpired(User user);
    Task BanUser(string userId, int noOfDays);
    Task UnbanUser(string userId);
    Task<Pagination<UserForListDto>> GetUsers(UserParameters userParameters, CancellationToken cancellationToken);
    Task<string> GetUserIdFromEmail(string email, CancellationToken cancellationToken);
    Task<UserInformationDto> GetLoggedUserInformationsByEmail(string email, CancellationToken cancellationToken);
    Task<IdentityResult> ConfrimUserEmail(User user, string token);
    Task<bool> IsUserBanned(string email);
    Task<UserDto> GetUserById(string userId, CancellationToken cancellationToken);
    Task<(string, DateTime)> GetEmailAndBanEndFromUserId(string userId, CancellationToken cancellationToken);
    Task<string> GetEmailFromUserId(string userId, CancellationToken cancellationToken);
    Task UpdateUser(UpdateUserDto updateUser, string profileImageUrl, string email);
    Task BanCheck(User user);
    Task<User> GetUserByEmail(string email);
}