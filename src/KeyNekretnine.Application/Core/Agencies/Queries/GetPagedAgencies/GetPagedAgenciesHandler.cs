using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Application.Core.Shared.Pagination;
using KeyNekretnine.Domain.Abstraction;
using System.Data;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetPagedAgencies;
internal sealed class GetPagedAgenciesHandler : IQueryHandler<GetPagedAgenciesQuery, Pagination<PagedAgencyResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetPagedAgenciesHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<Pagination<PagedAgencyResponse>>> Handle(GetPagedAgenciesQuery request, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<PagedAgencyResponse>(request.OrderBy);

        var sql = $"""
            SELECT 
                COUNT(a.id)
            FROM agencies AS a
            WHERE(@name = '' OR LOWER(a.name) LIKE '%' || LOWER(@name) || '%');

            SELECT
                a.id,
                a.name,
                a.created_date AS createdDate,
                COUNT(CASE WHEN ad.status = 1 THEN ad.id END) AS numberOfAdverts,
                a.email,
                a.image_url AS image,
                a.location_address AS address,
                a.phone_number AS phoneNumber,
                a.social_media_facebook AS facebook,
                a.social_media_instagram AS instagram,
                a.social_media_linkedin AS linkedin,
                a.social_media_twitter AS twitter
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

        var agencies = multi.Read<PagedAgencyResponse, SocialMediaResponse, PagedAgencyResponse>(
        (agency, socialMedia) =>
        {

            agency.SocialMedia = socialMedia;

            return agency;
        }, splitOn: "facebook").ToList();

        var metadata = new PagedList<PagedAgencyResponse>(agencies, count, request.PageNumber, request.PageSize);

        return new Pagination<PagedAgencyResponse> { Data = agencies, MetaData = metadata.MetaData };
    }
}
