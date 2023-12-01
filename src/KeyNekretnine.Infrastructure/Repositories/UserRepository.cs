using KeyNekretnine.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace KeyNekretnine.Infrastructure.Repositories;
internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<User?> GetByIdWithFavoriteAdvertsAsync(
    string userId,
    CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<User>()
            .Include(user => user.UserAdvertFavorites)
            .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
    }
}