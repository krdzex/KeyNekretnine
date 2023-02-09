using Contracts;
using Dapper;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Repository.RawQuery;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;
using System.Data;
using System.Security.Claims;

namespace Repository.Repositories;
internal sealed class UserRepository : IUserRepository
{
    private readonly DapperContext _dapperContext;
    private readonly UserManager<User> _userManager;
    public UserRepository(DapperContext dapperContext, UserManager<User> userManager)
    {
        _dapperContext = dapperContext;
        _userManager = userManager;
    }

    public async Task UserBanExpired(User user, CancellationToken cancellationToken)
    {

        user.BanEnd = null;
        user.IsBanned = false;

        await _userManager.UpdateAsync(user);
    }

    public async Task BanUser(string userId, int noOfDays)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        user.IsBanned = true;
        user.BanEnd = DateTime.Now.AddDays(Convert.ToDouble(noOfDays));

        await _userManager.UpdateAsync(user);
    }

    public async Task UnbanUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        user.IsBanned = false;
        user.BanEnd = null;

        await _userManager.UpdateAsync(user);
    }

    public async Task<Pagination<UserForListDto>> GetUsers(UserParameters userParameters)
    {
        var rawQuery = UserQuery.GetUsersQuery;

        var username = !string.IsNullOrEmpty(userParameters.Username) ?
            userParameters.Username.Trim().ToLower() : string.Empty;

        var skip = (userParameters.PageNumber - 1) * userParameters.PageSize;

        var param = new DynamicParameters();
        param.Add("skip", skip, DbType.Int32);
        param.Add("take", userParameters.PageSize, DbType.Int32);
        param.Add("username", username);

        using (var connection = _dapperContext.CreateConnection())
        {
            var multi = await connection.QueryMultipleAsync(rawQuery, param);
            var count = await multi.ReadSingleAsync<int>();
            var adverts = (await multi.ReadAsync<UserForListDto>()).ToList();

            var metadata = new PagedList<UserForListDto>(adverts, count, userParameters.PageNumber, userParameters.PageSize);

            return new Pagination<UserForListDto> { Data = adverts, MetaData = metadata.MetaData };
        }
    }

    public async Task<Pagination<UserForListDto>> GetBannedUsers(UserParameters userParameters)
    {
        var rawQuery = UserQuery.GetBannedUsersQuery;

        var username = !string.IsNullOrEmpty(userParameters.Username) ?
            userParameters.Username.Trim().ToLower() : string.Empty;

        var skip = (userParameters.PageNumber - 1) * userParameters.PageSize;

        var param = new DynamicParameters();
        param.Add("skip", skip, DbType.Int32);
        param.Add("take", userParameters.PageSize, DbType.Int32);
        param.Add("username", username, DbType.String);

        using (var connection = _dapperContext.CreateConnection())
        {
            var multi = await connection.QueryMultipleAsync(rawQuery, param);
            var count = await multi.ReadSingleAsync<int>();
            var adverts = (await multi.ReadAsync<UserForListDto>()).ToList();

            var metadata = new PagedList<UserForListDto>(adverts, count, userParameters.PageNumber, userParameters.PageSize);

            return new Pagination<UserForListDto> { Data = adverts, MetaData = metadata.MetaData };
        }

    }

    public async Task<string> GetUserIdFromEmail(string email)
    {
        var query = UserQuery.GetUserIdFromEmailQuery;

        var param = new DynamicParameters();
        param.Add("email", email, DbType.String);

        using (var connection = _dapperContext.CreateConnection())
        {
            var userId = await connection.QueryFirstOrDefaultAsync<string>(query, param);

            return userId;
        }
    }

    public async Task<UserInformationDto> GetLoggedUserInformations(IEnumerable<Claim> userClaims)
    {

        var query = UserQuery.GetLoggedUserInformations;

        var email = userClaims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;
        var roles = userClaims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value);

        var param = new DynamicParameters();
        param.Add("email", email, DbType.String);

        using (var connection = _dapperContext.CreateConnection())
        {
            var userInformations = await connection.QueryFirstOrDefaultAsync<UserInformationDto>(query, param);
            userInformations.Roles = roles;

            return userInformations;
        }
    }

    public async Task ConfrimUserEmail(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            throw new ArgumentException("Error while confirming email");
        }
    }

    public async Task<bool> IsUserBanned(string email)
    {
        var query = UserQuery.IsUserBannedQuery;

        var param = new DynamicParameters();
        param.Add("email", email, DbType.String);

        using (var connection = _dapperContext.CreateConnection())
        {
            var isBanned = await connection.QueryFirstOrDefaultAsync<bool>(query, param);

            return isBanned;
        }
    }
}
