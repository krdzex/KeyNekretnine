﻿using Dapper;
using KeyNekretnine.Application.Abstraction.Authentication;
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
    private readonly IUserContext _userContext;

    public GetPagedMyAdvertsHandler(ISqlConnectionFactory sqlConnectionFactory, IUserContext userContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _userContext = userContext;
    }

    public async Task<Result<Pagination<PagedMyAdvertResponse>>> Handle(GetPagedMyAdvertsQuery request, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<PagedMyAdvertResponse>(request.OrderBy);

        var purposeFilter = request.Purpose is not null ? $" AND a.purpose = {request.Purpose}" : "";
        var typeFilter = request.Type is not null ? $" AND a.type = {request.Type}" : "";
        var statusFilter = request.Status is not null && request.Status != 4 ? $" AND a.status = {request.Status}" : " AND a.status != 4";
        var agencyJoin = _userContext.AgencyId is not null ? "INNER JOIN agents ag on a.agent_id = ag.id" : "";
        var agencyFilter = _userContext.AgencyId is not null ? "WHERE ag.agency_id = @agencyId" : "WHERE a.user_id = @UserId";

        var sql = $"""
            SELECT
                COUNT(a.id)
            FROM adverts AS a
            {agencyJoin}
            {agencyFilter}
            {purposeFilter}
            {typeFilter}
            {statusFilter};

            SELECT 
            	a.reference_id AS referenceId,
            	a.created_on_date AS createdOnDate,
                CASE
                    WHEN au IS NULL THEN 'false'
                    ELSE 'true'
                END AS pendingUpdates,
            	a.cover_image_url AS coverImageUrl,
            	CONCAT(c.name, ', ', n.name) AS cityAndNeighborhood,
            	a.status,
            	a.type,
            	a.purpose,
                a.agent_id as agentId,
                a.updated_on_date AS updatedOnDate,
            	a.description_sr AS sr,
            	a.description_en AS en
            FROM adverts AS a
            INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
            INNER JOIN cities c ON n.city_id = c.id
            LEFT JOIN advert_updates AS au ON a.id = au.advert_id AND au.approved_on_date IS NULL AND au.rejected_on_date IS NULL
            {agencyJoin}
            {agencyFilter}
            {purposeFilter}
            {typeFilter}
            {statusFilter}
            ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;
            """;

        var skip = (request.PageNumber - 1) * request.PageSize;

        var param = new DynamicParameters();
        param.Add("skip", skip, DbType.Int32);
        param.Add("take", request.PageSize, DbType.Int32);
        param.Add("userId", _userContext.UserId, DbType.String);
        param.Add("agencyId", _userContext.AgencyId, DbType.Guid);

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