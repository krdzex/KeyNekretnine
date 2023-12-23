﻿using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;

namespace KeyNekretnine.Application.Core.Users.Queries.GetAboutUser;
internal sealed class GetAboutUserHandler : IQueryHandler<GetAboutUserQuery, AboutUserResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAboutUserHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<AboutUserResponse>> Handle(GetAboutUserQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                u.id,
                u.first_name AS firstName,
                u.last_name AS lastName,
                u.account_created_date AS accountCreatedDate,
                u.profile_image_url AS profileImageUrl,
                u.about,
                u.email,
                u.email_confirmed AS isEmailConfirmed,
                u.phone_number AS phoneNumber,
                u.is_agency AS isAgency,
                CASE 
                    WHEN u.password_hash IS NULL THEN 'false'
                    ELSE 'true'
                END AS canChangePassword
            FROM asp_net_users AS u
            WHERE id = @UserId
            """;

        var cmd = new CommandDefinition(sql, new { request.UserId }, cancellationToken: cancellationToken);

        var user = await connection.QueryFirstOrDefaultAsync<AboutUserResponse>(cmd);

        if (user is null)
        {
            return Result.Failure<AboutUserResponse>(UserErrors.NotFound);
        }

        return user;
    }
}