using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using Shared.CustomResponses;
using Shared.Halpers;
using Shared.RequestFeatures;
using System.Data;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencies;
internal sealed class GetAgenciesHandler : IQueryHandler<GetAgenciesQuery, Pagination<AgencyResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAgenciesHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<Pagination<AgencyResponse>>> Handle(GetAgenciesQuery request, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<AgencyResponse>(request.AgencyParameters.OrderBy, 'a');

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
                a.facebook_url AS facebookUrl,
                a.instagram_url AS instagramUrl,
                a.linkedin_url AS linkedinUrl,
                a.twitter_url AS twitterUrl,
                a.image_url AS imageUrl,
                a.address
            FROM agencies AS a
            LEFT JOIN agents ag ON ag.agency_id = a.id
            LEFT JOIN adverts ad ON ag.id = ad.agent_id
            WHERE(@name = '' OR LOWER(a.name) LIKE '%' || LOWER(@name) || '%')
            GROUP BY a.id
            ORDER BY {orderBy} OFFSET @Skip FETCH NEXT @Take ROWS ONLY;
            """;

        var name = !string.IsNullOrEmpty(request.AgencyParameters.Name) ?
            request.AgencyParameters.Name.Trim().ToLower() : string.Empty;
        var skip = (request.AgencyParameters.PageNumber - 1) * request.AgencyParameters.PageSize;

        var param = new DynamicParameters();
        param.Add("skip", skip, DbType.Int32);
        param.Add("take", request.AgencyParameters.PageSize, DbType.Int32);
        param.Add("name", name);

        using var connection = _sqlConnectionFactory.CreateConnection();
        var cmd = new CommandDefinition(sql, param, cancellationToken: cancellationToken);

        var multi = await connection.QueryMultipleAsync(cmd);
        var count = await multi.ReadSingleAsync<int>();
        var agencies = (await multi.ReadAsync<AgencyResponse>()).ToList();
        var metadata = new PagedList<AgencyResponse>(agencies, count, request.AgencyParameters.PageNumber, request.AgencyParameters.PageSize);

        return new Pagination<AgencyResponse> { Data = agencies, MetaData = metadata.MetaData };
    }
}
