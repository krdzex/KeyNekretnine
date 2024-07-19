using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertsCompare;
internal sealed class GetAdvertsCompareHandler : IQueryHandler<GetAdvertsCompareQuery, IReadOnlyList<CompareAdvertResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAdvertsCompareHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<CompareAdvertResponse>>> Handle(GetAdvertsCompareQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var referenceIds = new List<string> { request.FirstReferenceId, request.SecondReferenceId };

        const string sql = """
            SELECT
            	a.reference_id AS referenceId,
            	a.price,
            	a.floor_space AS floorSpace,
            	a.no_of_bedrooms AS noOfBedrooms,
            	a.no_of_bathrooms AS noOfBathrooms,
            	a.building_floor AS buildingFloor,
            	a.has_elevator AS hasElevator,
            	a.has_garage AS hasGarage,
            	a.has_terrace AS hasTerrace,
            	a.has_wifi AS hasWifi,
            	a.is_furnished AS isFurnished,
            	a.created_on_date AS createdOnDate,
            	a.year_of_building_created AS yearOfBuildingCreated,
            	a.cover_image_url AS coverImageUrl,
            	a.is_urgent,
            	a.is_under_construction,
                a.type,
                a.purpose,
                a.location_address AS address,
            	a.location_latitude AS latitude,
            	a.location_longitude AS longitude,
            	n.id AS neighborhoodId,
            	n.name AS neighborhoodName,
            	c.slug AS citySlug,
            	c.name as cityName
            FROM adverts a
            INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
            INNER JOIN cities c on n.city_id = c.id
            WHERE a.reference_id = ANY(@referenceIds)
            AND a.status = 1;
            """;

        var cmd = new CommandDefinition(sql, new { referenceIds }, cancellationToken: cancellationToken);

        var adverts = await connection.QueryAsync<CompareAdvertResponse, AdvertLocationResponse, CompareAdvertResponse>(
        sql,
        (advert, location) =>
        {
            advert.Location = location;

            return advert;

        }, new { referenceIds }, splitOn: "address");

        return adverts.ToList();
    }
}