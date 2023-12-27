using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Application.Core.Shared.Pagination;
using KeyNekretnine.Domain.Abstraction;
using System.Data;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetPagedMyAdverts;
internal sealed class GetPagedMyAdvertsHandler : IQueryHandler<GetPagedMyAdvertsQuery, Pagination<PagedMyAdvertResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetPagedMyAdvertsHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<Pagination<PagedMyAdvertResponse>>> Handle(GetPagedMyAdvertsQuery request, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<PagedMyAdvertResponse>(request.OrderBy);

        var purposeFilter = request.Purpose is not null ? $" AND a.purpose = {request.Purpose}" : "";
        var typeFilter = request.Type is not null ? $" AND a.type = {request.Type}" : "";
        var statusFilter = request.Status is not null && request.Status != 4 ? $" AND a.status = {request.Status}" : " AND a.status != 4";

        var sql = $"""
            SELECT
                COUNT(a.id)
            FROM adverts AS a
            WHERE a.user_id = @UserId
            {purposeFilter}
            {typeFilter}
            {statusFilter};

            SELECT 
            	a.reference_id AS referenceId,
            	a.created_on_date AS createdOnDate,
            	a.cover_image_url AS coverImageUrl,
            	CONCAT(c.name, ', ', n.name) AS cityAndNeighborhood,
            	a.status,
            	a.type,
            	a.purpose,
                a.updated_on_date AS updatedOnDate,
            	a.description_sr AS sr,
            	a.description_en AS en
            FROM adverts AS a
            INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
            INNER JOIN cities c ON n.city_id = c.id
            WHERE a.user_id = @UserId
            {purposeFilter}
            {typeFilter}
            {statusFilter}
            ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;
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

        var myAdverts = multi.Read<PagedMyAdvertResponse, AdvertDescriptionResponse, PagedMyAdvertResponse>(
            (advert, description) =>
            {
                advert.Description ??= description;

                return advert;
            }, splitOn: "sr").ToList();

        var metadata = new PagedList<PagedMyAdvertResponse>(myAdverts, count, request.PageNumber, request.PageSize);

        return new Pagination<PagedMyAdvertResponse> { Data = myAdverts, MetaData = metadata.MetaData };
    }
}