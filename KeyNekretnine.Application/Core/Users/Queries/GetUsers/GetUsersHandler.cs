using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Application.Core.Shared.Pagination;
using KeyNekretnine.Domain.Abstraction;
using System.Data;

namespace KeyNekretnine.Application.Core.Users.Queries.GetUsers;
internal sealed class GetUsersHandler : IQueryHandler<GetUsersQuery, Pagination<PaginationUserResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUsersHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<Pagination<PaginationUserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<PaginationUserResponse>(request.OrderBy);

        string banFilter = request.IsBanned switch
        {
            true => "AND (is_banned = true AND u.ban_end > now())",
            false => "AND (u.is_banned = false OR (u.is_banned = true AND u.ban_end < now()))",
            _ => ""
        };
        var sql = $"""
            SELECT COUNT(u.id)
            FROM asp_net_users AS u
            WHERE (@username = '' OR LOWER(u.User_name) LIKE '%' || LOWER(@username) || '%')
            {banFilter};

            SELECT
                u.id,
                u.email,
                u.user_name AS username,
                u.account_created_date AS accountCreatedDate,
                CASE
                    WHEN u.is_banned = true AND u.ban_end >= now() 
                        THEN true 
                    ELSE false 
                END AS isBanned,
                COUNT(a.id) AS numberOfAdverts
            FROM asp_net_users AS u
            LEFT JOIN adverts a ON a.user_id = u.id
            WHERE (@username = '' OR LOWER(u.User_name) LIKE '%' || LOWER(@username) || '%')
            {banFilter}
            GROUP BY u.id
            ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;
            """;

        var username = !string.IsNullOrEmpty(request.Username) ?
request.Username.Trim().ToLower() : string.Empty;

        var skip = (request.PageNumber - 1) * request.PageSize;

        var param = new DynamicParameters();
        param.Add("skip", skip, DbType.Int32);
        param.Add("take", request.PageSize, DbType.Int32);
        param.Add("username", username);

        using var connection = _sqlConnectionFactory.CreateConnection();
        var cmd = new CommandDefinition(sql, param, cancellationToken: cancellationToken);

        var multi = await connection.QueryMultipleAsync(cmd);
        var count = await multi.ReadSingleAsync<int>();
        var users = (await multi.ReadAsync<PaginationUserResponse>()).ToList();

        var metadata = new PagedList<PaginationUserResponse>(users, count, request.PageNumber, request.PageSize);

        return new Pagination<PaginationUserResponse> { Data = users, MetaData = metadata.MetaData };
    }
}