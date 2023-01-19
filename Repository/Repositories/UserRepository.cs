using Contracts;
using Dapper;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.RawQuery;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;
using System.Data;

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

    public async Task BanUser(string userId, int noOfDays)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException(userId);
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
            throw new UserNotFoundException(userId);
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

        var multi = await _context.Database.GetDbConnection().QueryMultipleAsync(rawQuery, param);
        var count = await multi.ReadSingleAsync<int>();
        var adverts = (await multi.ReadAsync<UserForListDto>()).ToList();

        var metadata = new PagedList<UserForListDto>(adverts, count, userParameters.PageNumber, userParameters.PageSize);

        return new Pagination<UserForListDto> { Data = adverts, MetaData = metadata.MetaData };
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
        param.Add("username", username);

        var multi = await _context.Database.GetDbConnection().QueryMultipleAsync(rawQuery, param);
        var count = await multi.ReadSingleAsync<int>();
        var adverts = (await multi.ReadAsync<UserForListDto>()).ToList();

        var metadata = new PagedList<UserForListDto>(adverts, count, userParameters.PageNumber, userParameters.PageSize);

        return new Pagination<UserForListDto> { Data = adverts, MetaData = metadata.MetaData };
    }
}
