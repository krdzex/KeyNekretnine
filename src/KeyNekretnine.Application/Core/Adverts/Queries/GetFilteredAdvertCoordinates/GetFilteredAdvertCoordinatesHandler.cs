using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Domain.Abstraction;
using System.Data;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetFilteredAdvertCoordinates;
internal sealed class GetFilteredAdvertCoordinatesHandler : IQueryHandler<GetFilteredAdvertCoordinatesQuery, IReadOnlyList<AdvertCoordinateResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetFilteredAdvertCoordinatesHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<AdvertCoordinateResponse>>> Handle(GetFilteredAdvertCoordinatesQuery request, CancellationToken cancellationToken)
    {
        var purposeFilter = request.Purpose is not null ? $" AND a.purpose = @Purpose" : "";
        var typeFilter = request.Type is not null ? $" AND a.type = @Type" : "";
        var bedroomsFilter = request.NoOfBedrooms is not null ? $" AND a.no_of_bedrooms = ANY(@NoOfBedrooms)" : "";
        var bathroomsFilter = request.NoOfBathrooms is not null ? $" AND a.no_of_bathrooms = ANY(@NoOfBathrooms)" : "";
        var neighborhoodFilter = request.Neighborhoods is not null ? $" AND a.neighborhood_id = ANY(@Neighborhoods)" : "";
        var urgentFilter = request.IsUrgent is not null ? $" AND a.is_urgent = @IsUrgent" : "";
        var underConstructionFilter = request.IsUnderConstruction is not null ? $" AND a.is_under_construction = @IsUnderConstruction" : "";
        var furnishedFilter = request.IsFurnished is not null ? $" AND a.is_furnished = @IsFurnished" : "";
        var cityFilter = request.CitySlug is not null ? $" AND c.slug = @CitySlug" : "";

        var sql = $"""
            SELECT
                a.reference_id AS referenceId,
                a.location_latitude AS latitude,
                a.location_longitude AS longitude
            FROM adverts a
            INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
            INNER JOIN cities c ON n.city_id = c.id
            WHERE a.price >= @minPrice AND a.price <= @maxPrice
            AND a.status = 1
            AND a.floor_space >= @minFloorSpace AND a.floor_space <= @maxFloorSpace
            {cityFilter}
            {purposeFilter}
            {typeFilter}
            {bedroomsFilter}
            {bathroomsFilter}
            {neighborhoodFilter}
            {urgentFilter}
            {underConstructionFilter}
            {furnishedFilter}
            """;

        var param = new DynamicParameters();
        param.Add("minPrice", request.MinPrice, DbType.Int32);
        param.Add("maxPrice", request.MaxPrice, DbType.Int32);
        param.Add("minFloorSpace", request.MinFloorSpace, DbType.Int32);
        param.Add("maxFloorSpace", request.MaxFloorSpace, DbType.Int32);
        param.Add("type", request.Type);
        param.Add("purpose", request.Purpose);
        param.Add("noOfBedrooms", request.NoOfBedrooms);
        param.Add("noOfBathrooms", request.NoOfBathrooms);
        param.Add("neighborhoods", request.Neighborhoods);
        param.Add("isUrgent", request.IsUrgent);
        param.Add("isUnderConstruction", request.IsUnderConstruction);
        param.Add("isFurnished", request.IsFurnished);
        param.Add("citySlug", request.CitySlug, DbType.String);


        using var connection = _sqlConnectionFactory.CreateConnection();

        var cmd = new CommandDefinition(sql, param, cancellationToken: cancellationToken);

        var coordinates = await connection.QueryAsync<AdvertCoordinateResponse>(cmd);


        return coordinates.ToList();
    }
}