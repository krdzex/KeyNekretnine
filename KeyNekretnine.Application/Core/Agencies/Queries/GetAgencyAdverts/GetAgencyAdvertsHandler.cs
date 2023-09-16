using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyAdverts;
internal sealed class GetAgencyAdvertsHandler : IQueryHandler<GetAgencyAdvertsQuery, IReadOnlyList<AgencyAdvertResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAgencyAdvertsHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<AgencyAdvertResponse>>> Handle(GetAgencyAdvertsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                a.id,
                a.price,
                a.floor_space AS FloorSpace,
                a.no_of_bedrooms AS NoOfBedrooms,
                a.no_of_bathrooms AS NoOfBathrooms,
                a.created_on_date AS CreatedOnDate,
                a.cover_image_url AS CoverImageUrl,
                CONCAT(c.name, ', ', n.name) AS location,
                a.type,
                a.purpose,
                a.location_address AS address,
                a.is_urgent AS IsUrgent,
                a.is_under_construction AS IsUnderConstruction,
                a.is_furnished AS IsFurnished
            FROM adverts AS a
            JOIN agents AS ia ON a.agent_id = ia.id
            JOIN neighborhoods n ON a.neighborhood_id = n.id
            JOIN cities c ON n.city_id = c.id
            WHERE ia.agency_id = @agencyId
            AND a.status = 1;
            """;

        var cmd = new CommandDefinition(sql, new { request.AgencyId }, cancellationToken: cancellationToken);

        var agencyAdverts = await connection.QueryAsync<AgencyAdvertResponse>(cmd);

        return agencyAdverts.ToList();
    }
}