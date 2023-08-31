using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Agents.Queries.GetAdverts;
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
                a.id,
                a.price,
                a.floor_space AS floorSpace,
                a.no_of_bedrooms AS noOfBedrooms,
                a.no_of_bathrooms AS noOfBathrooms,
                a.created_date AS createdDate,
                a.cover_image_url AS coverImageUrl,
                CONCAT(c.name, ', ', n.name) AS location,
                p.name_en AS purposeNameEn,
                p.name_sr AS purposeNameSr,
                t.name_sr AS typeNameSr,
                t.name_en AS typeNameEn,
                a.street,
                a.is_emergency AS isEmergency,
                a.is_under_construction AS isUnderConstruction,
                a.is_furnished AS isFurnished
            FROM adverts AS a
            JOIN advert_types t ON a.type_id = t.id
            JOIN advert_purposes p ON a.purpose_id = p.id
            JOIN neighborhoods n ON a.neighborhood_id = n.id
            JOIN cities c ON n.city_id = c.id
            WHERE a.agent_id = @agentId
            AND a.status_id = 1;
            """;

        var cmd = new CommandDefinition(sql, new { request.AgentId }, cancellationToken: cancellationToken);

        var agentAdverts = await connection.QueryAsync<AgentAdvertResponse>(cmd);

        return agentAdverts.ToList();
    }
}