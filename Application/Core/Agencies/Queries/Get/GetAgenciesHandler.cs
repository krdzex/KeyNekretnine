using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Application.Core.Shared.Pagination;
using KeyNekretnine.Domain.Abstraction;
using System.Data;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencies;
internal sealed class GetAgenciesHandler : IQueryHandler<GetAgenciesQuery, Pagination<PaginationAgencyResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAgenciesHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<Pagination<PaginationAgencyResponse>>> Handle(GetAgenciesQuery request, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<PaginationAgencyResponse>(request.OrderBy, 'a');

        var sql = $"""
            SELECT 
                COUNT(a.id)
            FROM agencies AS a
            WHERE(@name = '' OR LOWER(a.name) LIKE '%' || LOWER(@name) || '%');

            SELECT
                a.id,
                a.name,
                a.created_date AS createdDate,
                COUNT(ad.id) AS numAdverts,
                a.email,
                a.image_url AS image,
                a.address,
                a.facebook_url AS facebook,
                a.instagram_url AS instagram,
                a.linkedin_url AS linkedin,
                a.twitter_url AS twitter
            FROM agencies AS a
            LEFT JOIN agents ag ON ag.agency_id = a.id
            LEFT JOIN adverts ad ON ag.id = ad.agent_id
            WHERE(@name = '' OR LOWER(a.name) LIKE '%' || LOWER(@name) || '%')
            GROUP BY a.id
            ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;
            """;

        var name = !string.IsNullOrEmpty(request.Name) ?
            request.Name.Trim().ToLower() : string.Empty;
        var skip = (request.PageNumber - 1) * request.PageSize;

        var param = new DynamicParameters();
        param.Add("skip", skip, DbType.Int32);
        param.Add("take", request.PageSize, DbType.Int32);
        param.Add("name", name);

        using var connection = _sqlConnectionFactory.CreateConnection();
        var cmd = new CommandDefinition(sql, param, cancellationToken: cancellationToken);

        var multi = await connection.QueryMultipleAsync(cmd);
        var count = await multi.ReadSingleAsync<int>();

        var agencies = (multi.Read<PaginationAgencyResponse, SocialNetworkResponse, PaginationAgencyResponse>(
        (agency, social) =>
        {
            agency.SocialNetwork ??= social;
            return agency;
        }, splitOn: "facebook")).ToList();

        var metadata = new PagedList<PaginationAgencyResponse>(agencies, count, request.PageNumber, request.PageSize);

        return new Pagination<PaginationAgencyResponse> { Data = agencies, MetaData = metadata.MetaData };
    }
}
