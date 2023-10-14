using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgentAdverts;
internal sealed class GetAgentAdvertsHandler : IQueryHandler<GetAgentAdvertsQuery, IReadOnlyList<AgentAdvertResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAgentAdvertsHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<AgentAdvertResponse>>> Handle(GetAgentAdvertsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                a.reference_id AS referenceId,
                a.price,
                a.floor_space AS floorSpace,
                a.no_of_bedrooms AS noOfBedrooms,
                a.no_of_bathrooms AS noOfBathrooms,
                a.cover_image_url AS coverImageUrl,
                CONCAT(c.name, ', ', n.name) AS location,
                a.type,
                a.purpose,
                a.location_address AS address,
                a.is_urgent AS isUrgent,
                a.is_premium AS isPremium
            FROM adverts AS a
            JOIN neighborhoods n ON a.neighborhood_id = n.id
            JOIN cities c ON n.city_id = c.id
            WHERE a.agent_id = @agentId
            AND a.status = 1;
            """;

        var cmd = new CommandDefinition(sql, new { request.AgentId }, cancellationToken: cancellationToken);

        var agentAdverts = await connection.QueryAsync<AgentAdvertResponse>(cmd);

        return agentAdverts.ToList();
    }
}