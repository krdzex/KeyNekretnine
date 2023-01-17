using Entities.Models;

namespace Contracts;
public interface IUserRepository
{
    Task UserBanExpired(User user, CancellationToken cancellationToken);
}
