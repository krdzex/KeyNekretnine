using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Application.Core.Shared.Pagination;
using KeyNekretnine.Domain.Abstraction;
using System.Data;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetFavoriteAdverts;
internal sealed class GetFavoriteAdvertsHandler : IQueryHandler<GetFavoriteAdvertsQuery, Pagination<FavoriteAdvertResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetFavoriteAdvertsHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<Pagination<FavoriteAdvertResponse>>> Handle(GetFavoriteAdvertsQuery request, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<FavoriteAdvertResponse>(request.OrderBy);

        var sql = $"""
            SELECT
                COUNT(user_id)
            FROM user_advert_favorites
            WHERE user_id = @userId;

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
                a.type,
                a.purpose,
            	ua.created_favorite_date AS createdFavoriteDate
            FROM adverts a
            LEFT JOIN user_advert_favorites AS ua ON a.id = ua.advert_id
            INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
            INNER JOIN cities c ON n.city_id = c.id
            WHERE ua.user_id = @UserId
            ORDER BY {orderBy} OFFSET @skip FETCH NEXT @take ROWS ONLY;
            """;

        var skip = (request.PageNumber - 1) * request.PageSize;

        var param = new DynamicParameters();
        param.Add("skip", skip, DbType.Int32);
        param.Add("take", request.PageSize, DbType.Int32);
        param.Add("UserId", request.UserId, DbType.String);

        using var connection = _sqlConnectionFactory.CreateConnection();
        var cmd = new CommandDefinition(sql, param, cancellationToken: cancellationToken);

        var multi = await connection.QueryMultipleAsync(cmd);
        var count = await multi.ReadSingleAsync<int>();
        var favoriteAdverts = (await multi.ReadAsync<FavoriteAdvertResponse>()).ToList();

        var metadata = new PagedList<FavoriteAdvertResponse>(favoriteAdverts, count, request.PageNumber, request.PageSize);

        return new Pagination<FavoriteAdvertResponse> { Data = favoriteAdverts, MetaData = metadata.MetaData };
    }
}