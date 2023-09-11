//using Contracts;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System.Security.Claims;

//namespace KeyNekretnine.Api.Controllers.ActionFilters;
using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class BanUserChack : IAsyncActionFilter
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public BanUserChack(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userId = context.HttpContext.User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;


        var result = await IsUserBanned(userId);

        if (result)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        await next();
    }

    private async Task<bool> IsUserBanned(string userId)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                CASE 
                    WHEN u.is_banned = false OR (u.is_banned = true AND u.ban_end < now())
                        THEN false
                        ELSE true
                END AS is_banned
            FROM asp_net_users u
            WHERE u.id = @userId
            """;

        var isBanned = await connection.QueryFirstOrDefaultAsync<bool>(sql, new { userId });

        return isBanned;
    }
}