using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyAgents;
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
                a.id,
                a.first_name AS firstName,
                a.last_name AS lastName,
                a.image_url AS image,
                a.phone_number AS phoneNumber,
                a.email,
                COUNT(ad.id) AS numberOfAdverts
            FROM agents AS a
            LEFT JOIN adverts AS ad ON a.id = ad.agent_id AND ad.status = 1
            WHERE a.agency_id = @agencyId
            GROUP BY a.id;
            """;

        var cmd = new CommandDefinition(sql, new { request.AgencyId }, cancellationToken: cancellationToken);

        var agents = await connection.QueryAsync<AgencyAgentResponse>(cmd);

        return agents.ToList();
    }
}