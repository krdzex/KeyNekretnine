﻿using Dapper;
using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;

namespace KeyNekretnine.Application.Core.Users.Queries.GetCurrentUser;
internal sealed class GetCurrentUserHandler : IQueryHandler<GetCurrentUserQuery, CurrentUserResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IUserContext _userContext;

    public GetCurrentUserHandler(ISqlConnectionFactory sqlConnectionFactory, IUserContext userContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _userContext = userContext;
    }

    public async Task<Result<CurrentUserResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var usersDictionary = new Dictionary<string, CurrentUserResponse>();

        const string sql = """
            SELECT
                u.first_name AS firstName,
                u.last_name AS lastName,
                u.profile_image_url AS profileImageUrl,
                u.email,
                u.is_agency AS isAgency,
                a.name AS agencyName,
                a.id as AgencyId,
                r.name AS name
            FROM asp_net_users AS u
            LEFT JOIN asp_net_user_roles ur ON u.id = ur.user_id
            LEFT JOIN asp_net_roles r ON ur.role_id = r.id
            LEFT JOIN agencies a ON a.user_id = u.id
            WHERE u.id = @UserId
            """;

        var user = await connection.QueryAsync<CurrentUserResponse, string, CurrentUserResponse>(
            sql,
            (user, role) =>
            {
                if (!usersDictionary.TryGetValue(user.Email, out var currentUser))
                {
                    currentUser = user;
                    usersDictionary.Add(user.Email, currentUser);
                }

                if (role != null)
                {
                    currentUser.Roles.Add(role);
                }

                return currentUser;
            },
            new { _userContext.UserId },
            splitOn: "name"
        );

        var currentUser = usersDictionary.Values.FirstOrDefault();

        if (currentUser is null)
        {
            return Result.Failure<CurrentUserResponse>(UserErrors.NotFound);
        }

        return currentUser;
    }
}