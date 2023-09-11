using Contracts;

namespace KeyNekretnine.Infrastructure.Repositories;
internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    //public async Task BulkBan(List<string> userIds)
    //{
    //    await _dbContext.Set<User>()
    //        .Where(x => x.Id.)
    //        .ExecuteUpdateAsync(
    //        s => s.SetProperty(
    //            u => u.IsBanned = true));
    //}
    //public async Task UserBanExpired(User user)
    //{
    //    user.BanEnd = null;
    //    user.IsBanned = false;

    //    await _userManager.UpdateAsync(user);
    //}


    //public async Task<UserInformationDto> GetLoggedUserInformationsByEmail(string email, CancellationToken cancellationToken)
    //{

    //    var query = UserQuery.GetLoggedUserInformations;

    //    using (var connection = _dapperContext.CreateConnection())
    //    {
    //        var param = new DynamicParameters();
    //        param.Add("email", email, DbType.String);

    //        var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

    //        var userInformations = await connection.QueryFirstOrDefaultAsync<UserInformationDto>(cmd);

    //        return userInformations;
    //    }
    //}

    //public async Task<IdentityResult> ConfrimUserEmail(User user, string token)
    //{
    //    var result = await _userManager.ConfirmEmailAsync(user, token);

    //    return result;
    //}

    //public async Task<bool> IsUserBanned(string email)
    //{
    //    var query = UserQuery.IsUserBannedQuery;

    //    using (var connection = _dapperContext.CreateConnection())
    //    {
    //        var param = new DynamicParameters();
    //        param.Add("email", email, DbType.String);

    //        var isBanned = await connection.QueryFirstOrDefaultAsync<bool>(query, param);

    //        return isBanned;
    //    }
    //}

    //public async Task<UserDto> GetUserById(string userId, CancellationToken cancellationToken)
    //{
    //    var query = UserQuery.GetUserById;

    //    using (var connection = _dapperContext.CreateConnection())
    //    {
    //        var param = new DynamicParameters();
    //        param.Add("id", userId, DbType.String);

    //        var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

    //        var user = await connection.QueryFirstOrDefaultAsync<UserDto>(cmd);

    //        return user;
    //    }
    //}

    //public async Task<(string, DateTime)> GetEmailAndBanEndFromUserId(string userId, CancellationToken cancellationToken)
    //{
    //    var query = UserQuery.GetEmailAndBanEndDateFromUserId;

    //    using (var connection = _dapperContext.CreateConnection())
    //    {
    //        var param = new DynamicParameters();
    //        param.Add("id", userId, DbType.String);

    //        var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

    //        return await connection.QueryFirstOrDefaultAsync<(string, DateTime)>(cmd);
    //    }
    //}

    //public async Task<string> GetEmailFromUserId(string userId, CancellationToken cancellationToken)
    //{
    //    var query = UserQuery.GetEmailFromUserId;

    //    using (var connection = _dapperContext.CreateConnection())
    //    {
    //        var param = new DynamicParameters();
    //        param.Add("id", userId, DbType.String);

    //        var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

    //        return await connection.QueryFirstOrDefaultAsync<string>(cmd);
    //    }
    //}

    //public async Task<IdentityResult> UpdateUser(User user, UpdateUserDto updateUser)
    //{
    //    _mapper.Map(updateUser, user);

    //    var result = await _userManager.UpdateAsync(user);

    //    return result;
    //}

    //public async Task BanCheck(User user)
    //{
    //    if (user.IsBanned)
    //    {
    //        if (user.BanEnd > DateTime.UtcNow)
    //        {
    //            throw new UnauthorizedAccessException($"Banned until {user.BanEnd}");
    //        }
    //        await UserBanExpired(user);
    //    }
    //}

    //public async Task<User> GetUserByEmail(string email)
    //{
    //    var user = await _userManager.FindByEmailAsync(email);

    //    return user;
    //}

    //public async Task<User> GetUserProfileImageUrl(string email)
    //{
    //    var user = await _userManager.FindByEmailAsync(email);

    //    return user;
    //}

    //public async Task<string> GenerateEmailConfirmationToken(User user)
    //{
    //    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

    //    return token;
    //}
}