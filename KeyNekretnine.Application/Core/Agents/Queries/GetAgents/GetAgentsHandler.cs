using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Agents.Queries.GetAgentById;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Application.Core.Shared.Pagination;
using KeyNekretnine.Domain.Abstraction;
using System.Data;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgents;
internal sealed class GetAgentsHandler : IQueryHandler<GetAgentsQuery, Pagination<PaginationAgentResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAgentsHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<Pagination<PaginationAgentResponse>>> Handle(GetAgentsQuery request, CancellationToken cancellationToken)
    {
        var orderBy = OrderQueryBuilder.CreateOrderQuery<PaginationAgentResponse>(request.OrderBy);

        var sql = $"""
            SELECT
                COUNT(a.id)
            FROM agents AS a;

            SELECT 
                a.id,
                a.first_name AS firstName,
                COUNT(ad.id) AS numAdverts,
                a.last_Name AS lastName,
                a.email,
                a.image_Url AS imageUrl,
                a.social_media_twitter AS twitter,
                a.social_media_facebook AS facebook,
                a.social_media_instagram AS instagram,
                a.social_media_linkedin AS linkedin,
                ag.id AS agencyId,
                ag.name AS agencyName 
            FROM agents AS a
            LEFT JOIN adverts AS ad ON a.id = ad.agent_id AND ad.status_id = 1
            LEFT JOIN agencies as ag ON ag.id = a.agency_id
            GROUP BY a.id, ag.id
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
        var agents = multi.Read<PaginationAgentResponse, SocialMediaResponse, ShortAgencyResponse, PaginationAgentResponse>(
        (agent, socialMedia, agency) =>
        {

            agent.SocialMedia ??= socialMedia;
            agent.Agency = agency;
            return agent;
        }, splitOn: "twitter,agencyId").ToList();

        var metadata = new PagedList<PaginationAgentResponse>(agents, count, request.PageNumber, request.PageSize);

        return new Pagination<PaginationAgentResponse> { Data = agents, MetaData = metadata.MetaData };
    }
}