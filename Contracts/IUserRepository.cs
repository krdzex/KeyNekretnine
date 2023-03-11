using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;
using System.Security.Claims;

namespace Contracts;
public interface IUserRepository
{
    Task UserBanExpired(User user);
    Task BanUser(string userId, int noOfDays);
    Task UnbanUser(string userId);
    Task<Pagination<UserForListDto>> GetUsers(UserParameters userParameters, CancellationToken cancellationToken);
    Task<string> GetUserIdFromEmail(string email, CancellationToken cancellationToken);
    Task<UserInformationDto> GetLoggedUserInformations(IEnumerable<Claim> userClaims, CancellationToken cancellationToken);
    Task<IdentityResult> ConfrimUserEmail(string token, string email);
    Task<bool> IsUserBanned(string email);
    Task<UserDto> GetUser(string userId, CancellationToken cancellationToken);
    Task<(string, DateTime)> GetEmailAndBanEndFromUserId(string userId, CancellationToken cancellationToken);
    Task<string> GetEmailFromUserId(string userId, CancellationToken cancellationToken);
}