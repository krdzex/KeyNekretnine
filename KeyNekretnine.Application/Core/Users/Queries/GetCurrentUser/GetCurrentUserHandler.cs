using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;

namespace KeyNekretnine.Application.Core.Users.Queries.GetCurrentUser;
internal sealed class GetCurrentUserHandler : IQueryHandler<GetCurrentUserQuery, CurrentUserResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetCurrentUserHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<CurrentUserResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                u.first_name AS firstName,
                u.last_name AS lastName,
                u.profile_image_url AS profileImageUrl,
                u.email,
                u.is_agency AS IsAgency
            FROM asp_net_users AS u
            WHERE id = @UserId
            """;

        var cmd = new CommandDefinition(sql, new { request.UserId }, cancellationToken: cancellationToken);

        var currentUser = await connection.QueryFirstOrDefaultAsync<CurrentUserResponse>(cmd);

        if (currentUser is null)
        {
            return Result.Failure<CurrentUserResponse>(UserErrors.NotFound);
        }

        return currentUser;
    }
}