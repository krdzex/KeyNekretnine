using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAvgPricePerSqftInRadius;
internal sealed class GetAvgPricePerSqftInRadiusHandler : IQueryHandler<GetAvgPricePerSqftInRadiusQuery, AvgPricePerSqftResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAvgPricePerSqftInRadiusHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }


    public async Task<Result<AvgPricePerSqftResponse>> Handle(GetAvgPricePerSqftInRadiusQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var rediusInMeters = 1000.00;

        if (request.RadiusInKm is not null)
        {
            rediusInMeters = request.RadiusInKm.Value * 1000.00;
        }

        const string sql = """
            WITH selected_advert AS (
                SELECT location_latitude, location_longitude 
                FROM adverts 
                WHERE reference_id = @referenceId
                LIMIT 1
            )
            SELECT
                ROUND(AVG(a.price / NULLIF(a.floor_space, 0))::NUMERIC) AS pricePerSquareFoot
            FROM
                adverts a,
                selected_advert sa
            WHERE
                a.reference_id <> @referenceId
                AND a.status = 1
                AND earth_box(
                    ll_to_earth(sa.location_latitude, sa.location_longitude), @rediusInMeters
                ) @> ll_to_earth(a.location_latitude, a.location_longitude)
                AND earth_distance(
                    ll_to_earth(a.location_latitude, a.location_longitude), 
                    ll_to_earth(sa.location_latitude, sa.location_longitude)
                ) < @rediusInMeters
                AND a.floor_space > 0;
            """;

        var cmd = new CommandDefinition(sql, new { request.ReferenceId, rediusInMeters }, cancellationToken: cancellationToken);

        var avgPricePerSqft = await connection.QueryFirstOrDefaultAsync<AvgPricePerSqftResponse>(cmd);

        return avgPricePerSqft;
    }
}