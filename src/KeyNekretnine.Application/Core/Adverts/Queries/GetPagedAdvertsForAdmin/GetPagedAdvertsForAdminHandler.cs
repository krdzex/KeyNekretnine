using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Application.Core.Shared.Pagination;
using KeyNekretnine.Domain.Abstraction;
using System.Data;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdvertsForAdmin;
internal sealed class GetPagedAdvertsForAdminHandler : IQueryHandler<GetPagedAdvertsForAdminQuery, Pagination<PagedAdvertForAdminResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetPagedAdvertsForAdminHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<Pagination<PagedAdvertForAdminResponse>>> Handle(GetPagedAdvertsForAdminQuery request, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<PagedAdvertForAdminResponse>(request.OrderBy);

        var purposeFilter = request.Purpose is not null ? $" AND a.purpose = {request.Purpose}" : "";
        var typeFilter = request.Type is not null ? $" AND a.type = {request.Type}" : "";
        var statusFilter = request.Status is not null ? $" AND a.status = {request.Status}" : " AND a.status = <> 4";

        var sql = $"""
            SELECT
                COUNT(a.id)
            FROM adverts AS a
            WHERE(@referenceId = '' OR LOWER(a.reference_id) LIKE '%' || LOWER(@referenceId) || '%')
            {purposeFilter}
            {typeFilter}
            {statusFilter};

            SELECT 
            	a.reference_id AS referenceId,
            	a.price,
            	a.no_of_bedrooms AS noOfBedrooms,
            	a.no_of_bathrooms AS noOfBathrooms,
            	a.created_on_date AS createdOnDate,
            	a.purpose,
            	a.type,
            	a.status,
            	CONCAT(c.name, ', ', n.name) AS cityAndNeighborhood
            FROM adverts AS a
            INNER JOIN neighborhoods AS n ON a.neighborhood_id = n.id
            INNER JOIN cities AS c ON n.city_id = c.id
            WHERE(@referenceId = '' OR LOWER(a.reference_id) LIKE '%' || LOWER(@referenceId) || '%')
            {purposeFilter}
            {typeFilter}
            {statusFilter}
            ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;
            """;

        var referenceId = !string.IsNullOrEmpty(request.ReferenceId) ?
            request.ReferenceId.Trim().ToLower() : string.Empty;

        var skip = (request.PageNumber - 1) * request.PageSize;

        var param = new DynamicParameters();
        param.Add("skip", skip, DbType.Int32);
        param.Add("take", request.PageSize, DbType.Int32);
        param.Add("referenceId", referenceId, DbType.String);

        using var connection = _sqlConnectionFactory.CreateConnection();
        var cmd = new CommandDefinition(sql, param, cancellationToken: cancellationToken);

        var multi = await connection.QueryMultipleAsync(cmd);
        var count = await multi.ReadSingleAsync<int>();
        var adverts = (await multi.ReadAsync<PagedAdvertForAdminResponse>()).ToList();

        var metadata = new PagedList<PagedAdvertForAdminResponse>(adverts, count, request.PageNumber, request.PageSize);

        return new Pagination<PagedAdvertForAdminResponse> { Data = adverts, MetaData = metadata.MetaData };
    }
}