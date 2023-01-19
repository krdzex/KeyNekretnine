using Entities.Models;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;

namespace Contracts;
public interface IUserRepository
{
    Task UserBanExpired(User user, CancellationToken cancellationToken);
    Task BanUser(string userId, int noOfDays);
    Task UnbanUser(string userId);
    Task<Pagination<UserForListDto>> GetUsers(UserParameters userParameters);
    Task<Pagination<UserForListDto>> GetBannedUsers(UserParameters userParameters);
}
