using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Agencies.Queries.GetLocation;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyLocation;
internal sealed class GetAgencyHandler : IQueryHandler<GetAgencyLocationQuery, AgencyLocationResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAgencyHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<AgencyLocationResponse>> Handle(GetAgencyLocationQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                latitude,
                longitude,
                address
            FROM agencies
            WHERE id = @agencyId;
            """;

        var cmd = new CommandDefinition(sql, new { request.AgencyId }, cancellationToken: cancellationToken);

        var agencyLocation = await connection.QueryFirstOrDefaultAsync<AgencyLocationResponse>(cmd);

        return agencyLocation;
    }
}