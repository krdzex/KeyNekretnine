using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Repository.Repositories;
internal sealed class UserRepository : IUserRepository
{
    private readonly RepositoryContext _context;
    private readonly UserManager<User> _userManager;
    public UserRepository(RepositoryContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task UserBanExpired(User user, CancellationToken cancellationToken)
    {

        user.BanEnd = null;
        user.IsBanned = false;

        await _userManager.UpdateAsync(user);
    }

    //public async Task BanUser(Guid id, int noOfDays)
    //{
    //}
};

