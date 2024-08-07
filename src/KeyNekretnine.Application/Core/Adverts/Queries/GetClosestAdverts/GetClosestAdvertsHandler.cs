using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetClosestAdverts;
internal sealed class GetClosestAdvertsHandler : IQueryHandler<GetClosestAdvertsQuery, IReadOnlyList<ClosestAdvertsResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetClosestAdvertsHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }


    public async Task<Result<IReadOnlyList<ClosestAdvertsResponse>>> Handle(GetClosestAdvertsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            WITH selected_advert AS (
                SELECT location_latitude, location_longitude 
                FROM adverts 
                WHERE reference_id = @referenceId
                LIMIT 1
            )
            SELECT
                a.reference_id AS referenceId,
                a.price,
                a.floor_space AS floorSpace,
                a.no_of_bedrooms AS noOfBedrooms,
                a.no_of_bathrooms AS noOfBathrooms,
                a.created_on_date AS createdOnDate,
                a.cover_image_url AS coverImageUrl,
                CONCAT(c.name, ', ', n.name) AS cityAndNeighborhood,
                a.location_address AS address,
                a.is_urgent AS isUrgent,
                a.is_premium AS isPremium,
                a.type,
                a.purpose,
                ROUND(earth_distance(
                    ll_to_earth(a.location_latitude, a.location_longitude), 
                    ll_to_earth(sa.location_latitude, sa.location_longitude)
                )::NUMERIC) AS distanceMeters
            FROM
                adverts a
                INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
                INNER JOIN cities c ON n.city_id = c.id,
                selected_advert sa
            WHERE
                a.reference_id <> @referenceId
                AND a.status = 1
                AND earth_box(
                    ll_to_earth(sa.location_latitude, sa.location_longitude), 1000
                ) @> ll_to_earth(a.location_latitude, a.location_longitude)
                AND earth_distance(
                    ll_to_earth(a.location_latitude, a.location_longitude), 
                    ll_to_earth(sa.location_latitude, sa.location_longitude)
                ) < 1000
            ORDER BY
                distanceMeters
            LIMIT 8;
            """;

        var cmd = new CommandDefinition(sql, new { request.ReferenceId }, cancellationToken: cancellationToken);

        var closestAdverts = await connection.QueryAsync<ClosestAdvertsResponse>(cmd);

        return closestAdverts.ToList();
    }
}