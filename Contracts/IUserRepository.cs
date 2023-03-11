using Entities.Models;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;
using System.Security.Claims;

namespace Contracts;
public interface IUserRepository
{
    Task UserBanExpired(User user, CancellationToken cancellationToken);
    Task BanUser(string userId, int noOfDays, CancellationToken cancellationToken);
    Task UnbanUser(string userId, CancellationToken cancellationToken);
    Task<Pagination<UserForListDto>> GetUsers(UserParameters userParameters, CancellationToken cancellationToken);
    Task<string> GetUserIdFromEmail(string email, CancellationToken cancellationToken);
    Task<UserInformationDto> GetLoggedUserInformations(IEnumerable<Claim> userClaims, CancellationToken cancellationToken);
    Task ConfrimUserEmail(string token, string email, CancellationToken cancellationToken);
    Task<bool> IsUserBanned(string email);
    Task<UserDto> GetUser(string userId, CancellationToken cancellationToken);
    Task<(string, DateTime)> GetEmailAndBanEndFromUserId(string userId, CancellationToken cancellationToken);
    Task<string> GetEmailFromUserId(string userId, CancellationToken cancellationToken);
}