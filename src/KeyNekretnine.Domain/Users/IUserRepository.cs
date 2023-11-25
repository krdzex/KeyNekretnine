namespace KeyNekretnine.Domain.Users;
public interface IUserRepository
{
    Task<User?> GetByIdWithFavoriteAdvertsAsync(string userId, CancellationToken cancellationToken = default);
}