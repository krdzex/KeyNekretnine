﻿using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyLocation;
internal sealed class GetAgencyLocationHandler : IQueryHandler<GetAgencyLocationQuery, AgencyLocationResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAgencyLocationHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<AgencyLocationResponse>> Handle(GetAgencyLocationQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                location_latitude AS latitude,
                location_longitude AS longitude,
                location_address AS address
            FROM agencies
            WHERE id = @agencyId;
            """;

        var cmd = new CommandDefinition(sql, new { request.AgencyId }, cancellationToken: cancellationToken);

        var agencyLocation = await connection.QueryFirstOrDefaultAsync<AgencyLocationResponse>(cmd);

        return agencyLocation;
    }
}