using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Application.Core.Shared.Pagination;
using KeyNekretnine.Domain.Abstraction;
using System.Data;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertUpdates;
internal sealed class GetAdvertUpdatesHandler : IQueryHandler<GetAdvertUpdatesQuery, Pagination<AdvertForUpdateResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAdvertUpdatesHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<Pagination<AdvertForUpdateResponse>>> Handle(GetAdvertUpdatesQuery request, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<AdvertForUpdateResponse>(request.OrderBy);

        var updateTypeFilter = request.UpdateType is not null ? " AND au.type = @UpdateType" : "";
        var referenceIdFilter = request.ReferenceId is not null ? " AND a.reference_id = @ReferenceId" : "";

        var sql = $"""
            SELECT
                COUNT(au.id)
            FROM advert_updates AS au
            INNER JOIN adverts AS a ON a.id = au.advert_id
            WHERE au.new_content IS NOT NULL
            {updateTypeFilter}
            {referenceIdFilter};

            SELECT 
            	au.id,
                au.type AS updateType,
                a.reference_id AS referenceId,
                au.created_on_date AS createdOnDate,
                CASE
                    WHEN au.approved_on_date IS NOT NULL OR au.rejected_on_date IS NOT NULL THEN TRUE
                    ELSE FALSE
                END AS isProcessed
            FROM advert_updates AS au
            INNER JOIN adverts AS a ON a.id = au.advert_id
            WHERE au.new_content IS NOT NULL
            {updateTypeFilter}
            {referenceIdFilter}
            ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;
            """;

        var skip = (request.PageNumber - 1) * request.PageSize;

        var param = new DynamicParameters();
        param.Add("skip", skip, DbType.Int32);
        param.Add("take", request.PageSize, DbType.Int32);
        param.Add("updateType", request.UpdateType, DbType.Int16);
        param.Add("referenceId", request.ReferenceId, DbType.String);

        using var connection = _sqlConnectionFactory.CreateConnection();
        var cmd = new CommandDefinition(sql, param, cancellationToken: cancellationToken);

        var multi = await connection.QueryMultipleAsync(cmd);
        var count = await multi.ReadSingleAsync<int>();
        var updates = (await multi.ReadAsync<AdvertForUpdateResponse>()).ToList();

        var metadata = new PagedList<AdvertForUpdateResponse>(updates, count, request.PageNumber, request.PageSize);

        return new Pagination<AdvertForUpdateResponse> { Data = updates, MetaData = metadata.MetaData };
    }
}