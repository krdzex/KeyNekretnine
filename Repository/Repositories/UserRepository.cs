//using AutoMapper;
//using Contracts;
//using Dapper;
//using Entities.Models;
//using Microsoft.AspNetCore.Identity;
//using Repository.RawQuery;
//using Shared.CustomResponses;
//using Shared.DataTransferObjects.User;
//using Shared.Halpers;
//using Shared.RequestFeatures;
//using System.Data;

//namespace Repository.Repositories;
//internal sealed class UserRepository : IUserRepository
//{
//    private readonly DapperContext _dapperContext;
//    private readonly UserManager<User> _userManager;
//    private readonly IMapper _mapper;
//    public UserRepository(DapperContext dapperContext, UserManager<User> userManager, IMapper mapper)
//    {
//        _dapperContext = dapperContext;
//        _userManager = userManager;
//        _mapper = mapper;
//    }

//    public async Task UserBanExpired(User user)
//    {
//        user.BanEnd = null;
//        user.IsBanned = false;

//        await _userManager.UpdateAsync(user);
//    }

//    public async Task<IdentityResult> BanUser(User user, int noOfDays)
//    {
//        user.IsBanned = true;
//        user.BanEnd = DateTime.Now.AddDays(Convert.ToDouble(noOfDays));

//        var banResult = await _userManager.UpdateAsync(user);

//        return banResult;
//    }

//    public async Task<IdentityResult> UnbanUser(User user)
//    {

//        user.IsBanned = false;
//        user.BanEnd = null;

//        var unbanResult = await _userManager.UpdateAsync(user);

//        return unbanResult;
//    }

//    public async Task<Pagination<UserForListDto>> GetUsers(UserParameters userParameters, CancellationToken cancellationToken)
//    {
//        var orderBy = OrderQueryBuilder.CreateOrderQuery<UserForListDto>(userParameters.OrderBy, 'u');

//        var query = UserQuery.MakeUsersQuery(userParameters.IsBanned, orderBy);

//        var username = !string.IsNullOrEmpty(userParameters.Username) ?
//            userParameters.Username.Trim().ToLower() : string.Empty;

//        var skip = (userParameters.PageNumber - 1) * userParameters.PageSize;

//        var param = new DynamicParameters();
//        param.Add("skip", skip, DbType.Int32);
//        param.Add("take", userParameters.PageSize, DbType.Int32);
//        param.Add("username", username);

//        using (var connection = _dapperContext.CreateConnection())
//        {
//            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

//            var multi = await connection.QueryMultipleAsync(cmd);
//            var count = await multi.ReadSingleAsync<int>();
//            var adverts = (await multi.ReadAsync<UserForListDto>()).ToList();

//            var metadata = new PagedList<UserForListDto>(adverts, count, userParameters.PageNumber, userParameters.PageSize);

//            return new Pagination<UserForListDto> { Data = adverts, MetaData = metadata.MetaData };
//        }
//    }

//    public async Task<string> GetUserIdFromEmail(string email, CancellationToken cancellationToken)
//    {
//        var query = UserQuery.GetUserIdFromEmailQuery;

//        using (var connection = _dapperContext.CreateConnection())
//        {

//            var param = new DynamicParameters();
//            param.Add("email", email, DbType.String);

//            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

//            var userId = await connection.QueryFirstOrDefaultAsync<string>(cmd);

//            return userId;
//        }
//    }

//    public async Task<UserInformationDto> GetLoggedUserInformationsByEmail(string email, CancellationToken cancellationToken)
//    {

//        var query = UserQuery.GetLoggedUserInformations;

//        using (var connection = _dapperContext.CreateConnection())
//        {
//            var param = new DynamicParameters();
//            param.Add("email", email, DbType.String);

//            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

//            var userInformations = await connection.QueryFirstOrDefaultAsync<UserInformationDto>(cmd);

//            return userInformations;
//        }
//    }

//    public async Task<IdentityResult> ConfrimUserEmail(User user, string token)
//    {
//        var result = await _userManager.ConfirmEmailAsync(user, token);

//        return result;
//    }

//    public async Task<bool> IsUserBanned(string email)
//    {
//        var query = UserQuery.IsUserBannedQuery;

//        using (var connection = _dapperContext.CreateConnection())
//        {
//            var param = new DynamicParameters();
//            param.Add("email", email, DbType.String);

//            var isBanned = await connection.QueryFirstOrDefaultAsync<bool>(query, param);

//            return isBanned;
//        }
//    }

//    public async Task<UserDto> GetUserById(string userId, CancellationToken cancellationToken)
//    {
//        var query = UserQuery.GetUserById;

//        using (var connection = _dapperContext.CreateConnection())
//        {
//            var param = new DynamicParameters();
//            param.Add("id", userId, DbType.String);

//            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

//            var user = await connection.QueryFirstOrDefaultAsync<UserDto>(cmd);

//            return user;
//        }
//    }

//    public async Task<(string, DateTime)> GetEmailAndBanEndFromUserId(string userId, CancellationToken cancellationToken)
//    {
//        var query = UserQuery.GetEmailAndBanEndDateFromUserId;

//        using (var connection = _dapperContext.CreateConnection())
//        {
//            var param = new DynamicParameters();
//            param.Add("id", userId, DbType.String);

//            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

//            return await connection.QueryFirstOrDefaultAsync<(string, DateTime)>(cmd);
//        }
//    }

//    public async Task<string> GetEmailFromUserId(string userId, CancellationToken cancellationToken)
//    {
//        var query = UserQuery.GetEmailFromUserId;

//        using (var connection = _dapperContext.CreateConnection())
//        {
//            var param = new DynamicParameters();
//            param.Add("id", userId, DbType.String);

//            var cmd = new CommandDefinition(query, param, cancellationToken: cancellationToken);

//            return await connection.QueryFirstOrDefaultAsync<string>(cmd);
//        }
//    }

//    public async Task<IdentityResult> UpdateUser(User user, UpdateUserDto updateUser)
//    {
//        _mapper.Map(updateUser, user);

//        var result = await _userManager.UpdateAsync(user);

//        return result;
//    }

//    public async Task BanCheck(User user)
//    {
//        if (user.IsBanned)
//        {
//            if (user.BanEnd > DateTime.UtcNow)
//            {
//                throw new UnauthorizedAccessException($"Banned until {user.BanEnd}");
//            }
//            await UserBanExpired(user);
//        }
//    }

//    public async Task<User> GetUserByEmail(string email)
//    {
//        var user = await _userManager.FindByEmailAsync(email);

//        return user;
//    }

//    public async Task<User> GetUserProfileImageUrl(string email)
//    {
//        var user = await _userManager.FindByEmailAsync(email);

//        return user;
//    }

//    public async Task<string> GenerateEmailConfirmationToken(User user)
//    {
//        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

//        return token;
//    }
//}