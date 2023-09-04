using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgents;
internal sealed class GetAgencyAgentsHandler : IQueryHandler<GetAgencyAgentsQuery, IReadOnlyList<AgencyAgentResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAgencyAgentsHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<AgencyAgentResponse>>> Handle(GetAgencyAgentsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                a.first_name AS FirstName,
                a.last_name AS LastName,
                a.image_url AS ImageUrl
            FROM agents AS a
            WHERE a.agency_id = @agencyId;
            """;

        var cmd = new CommandDefinition(sql, new { request.AgencyId }, cancellationToken: cancellationToken);

        var agents = await connection.QueryAsync<AgencyAgentResponse>(cmd);

        return agents.ToList();
    }
}