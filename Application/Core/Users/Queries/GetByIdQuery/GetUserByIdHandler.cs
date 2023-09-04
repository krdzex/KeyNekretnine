using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;

namespace KeyNekretnine.Application.Core.Users.Queries.GetByIdQuery;
internal sealed class GetUserByIdHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUserByIdHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                u.first_name AS firstName,
                u.last_name AS lastName,
                u.account_created_date AS accounCreatedDate,
                u.id,
                u.email,
                CASE
                    WHEN u.is_banned = true AND u.ban_end >= now() 
                        THEN true 
                    ELSE false 
                END AS isBanned
            FROM asp_net_users AS u
            WHERE id = @UserId
            """;

        var cmd = new CommandDefinition(sql, new { request.UserId }, cancellationToken: cancellationToken);

        var user = await connection.QueryFirstOrDefaultAsync<UserResponse>(cmd);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound);
        }

        return user;
    }
}