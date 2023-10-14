using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Application.Core.Shared.Pagination;
using KeyNekretnine.Domain.Abstraction;
using System.Data;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdvertReportsForAdmin;
internal sealed class GetPagedAdvertReportsForAdminHandler : IQueryHandler<GetPagedAdvertReportsForAdminQuery, Pagination<PagedAdvertReport>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetPagedAdvertReportsForAdminHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<Pagination<PagedAdvertReport>>> Handle(GetPagedAdvertReportsForAdminQuery request, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<PagedAdvertReport>(request.OrderBy);

        var sql = $"""
            SELECT COUNT(DISTINCT advert_id)
            FROM user_advert_reports;

            SELECT 
            	a.reference_id AS referenceId,
            	SUM(CASE WHEN r.reject_reason_id = 1 THEN 1 ELSE 0 END) AS repeatingAdvert,
                SUM(CASE WHEN r.reject_reason_id = 2 THEN 1 ELSE 0 END) AS badImages,
                SUM(CASE WHEN r.reject_reason_id = 3 THEN 1 ELSE 0 END) AS badInformations,
                COUNT(advert_id) AS allReports
            FROM user_advert_reports AS r
            INNER JOIN adverts AS a ON r.advert_id = a.id
            GROUP BY referenceId
            ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;
            """;

        var skip = (request.PageNumber - 1) * request.PageSize;

        var param = new DynamicParameters();
        param.Add("skip", skip, DbType.Int32);
        param.Add("take", request.PageSize, DbType.Int32);

        using var connection = _sqlConnectionFactory.CreateConnection();
        var cmd = new CommandDefinition(sql, param, cancellationToken: cancellationToken);

        var multi = await connection.QueryMultipleAsync(cmd);
        var count = await multi.ReadSingleAsync<int>();
        var advertReports = (await multi.ReadAsync<PagedAdvertReport>()).ToList();

        var metadata = new PagedList<PagedAdvertReport>(advertReports, count, request.PageNumber, request.PageSize);

        return new Pagination<PagedAdvertReport> { Data = advertReports, MetaData = metadata.MetaData };
    }
}