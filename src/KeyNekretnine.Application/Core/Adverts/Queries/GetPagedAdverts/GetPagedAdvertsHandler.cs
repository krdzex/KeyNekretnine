﻿using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Application.Core.Shared.Pagination;
using KeyNekretnine.Domain.Abstraction;
using System.Data;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdverts;
internal sealed class GetPagedAdvertsHandler : IQueryHandler<GetPagedAdvertsQuery, Pagination<PagedAdvertResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetPagedAdvertsHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<Pagination<PagedAdvertResponse>>> Handle(GetPagedAdvertsQuery request, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<PagedAdvertResponse>(request.OrderBy);

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
                COUNT(a.id)
            FROM adverts AS a
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
            {furnishedFilter};

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
                a.purpose
            FROM adverts AS a
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
            ORDER BY 
            CASE 
                WHEN is_premium = 'true' AND agent_id IS NOT NULL THEN 1
                WHEN agent_id IS NOT NULL THEN 2
                WHEN is_premium = 'true' THEN 3
                ELSE 4
            END,
            {orderBy} 
            OFFSET @skip FETCH NEXT @take ROWS ONLY;
            """;

        var skip = (request.PageNumber - 1) * request.PageSize;

        var param = new DynamicParameters();
        param.Add("skip", skip, DbType.Int32);
        param.Add("take", request.PageSize, DbType.Int32);
        param.Add("minPrice", request.MinPrice, DbType.Int32);
        param.Add("maxPrice", request.MaxPrice, DbType.Int32);
        param.Add("minFloorSpace", request.MinFloorSpace, DbType.Int32);
        param.Add("maxFloorSpace", request.MaxFloorSpace, DbType.Int32);
        param.Add("type", request.Type);
        param.Add("purpose", request.Purpose);
        param.Add("noOfBedrooms", request.NoOfBedrooms);
        param.Add("noOfBathrooms", request.NoOfBathrooms);
        param.Add("noOfBathrooms", request.NoOfBathrooms);
        param.Add("neighborhoods", request.Neighborhoods);
        param.Add("isUrgent", request.IsUrgent);
        param.Add("isUnderConstruction", request.IsUnderConstruction);
        param.Add("isFurnished", request.IsFurnished);
        param.Add("citySlug", request.CitySlug, DbType.String);


        using var connection = _sqlConnectionFactory.CreateConnection();

        var cmd = new CommandDefinition(sql, param, cancellationToken: cancellationToken);

        var multi = await connection.QueryMultipleAsync(cmd);
        var count = await multi.ReadSingleAsync<int>();
        var adverts = (await multi.ReadAsync<PagedAdvertResponse>()).ToList();

        var metadata = new PagedList<PagedAdvertResponse>(adverts, count, request.PageNumber, request.PageSize);

        return new Pagination<PagedAdvertResponse> { Data = adverts, MetaData = metadata.MetaData };
    }
}