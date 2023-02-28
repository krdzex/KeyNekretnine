using Entities.Models;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;
using System.Security.Claims;

namespace Contracts;
public interface IUserRepository
{
    Task UserBanExpired(User user, CancellationToken cancellationToken);
    Task BanUser(string userId, int noOfDays);
    Task UnbanUser(string userId);
    Task<Pagination<UserForListDto>> GetUsers(UserParameters userParameters);
    Task<string> GetUserIdFromEmail(string email);
    Task<UserInformationDto> GetLoggedUserInformations(IEnumerable<Claim> userClaims);
    Task ConfrimUserEmail(string token, string email);
    Task<bool> IsUserBanned(string email);
}