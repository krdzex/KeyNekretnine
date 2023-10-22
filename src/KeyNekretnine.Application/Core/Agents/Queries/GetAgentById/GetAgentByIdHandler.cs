using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Language.Queries.GetLanguages;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agents;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgentById;
internal sealed class GetAgentByIdHandler : IQueryHandler<GetAgentByIdQuery, AgentResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAgentByIdHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<AgentResponse>> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        Dictionary<Guid, AgentResponse> agentsDictionary = new();

        const string sql = """
            SELECT
                a.id,
                a.first_name AS firstName,
                a.last_Name AS lastName,
                a.email,
                a.image_Url AS imageUrl,
                a.description,
                a.phone_number AS phoneNumber,
                a.social_media_twitter AS twitter,
                a.social_media_facebook AS facebook,
                a.social_media_instagram AS instagram,
                a.social_media_linkedin AS linkedin,
                ag.id,
                ag.name,
                l.name
            FROM agents AS a
            LEFT JOIN agencies as ag ON ag.id = a.agency_id
            LEFT JOIN agent_languages al ON al.agent_id = a.id
            LEFT JOIN languages l ON al.language_id = l.id
            WHERE a.id = @AgentId;
            """;

        var agent = await connection.QueryAsync<AgentResponse, SocialMediaResponse, ShortAgencyResponse, LanguageResponse, AgentResponse>(
            sql,
            (agent, socialMedia, agency, language) =>
        {
            if (agentsDictionary.TryGetValue(agent.Id, out var existingAgent))
            {
                agent = existingAgent;
            }
            else
            {
                agentsDictionary.Add(agent.Id, agent);
            }
            if (socialMedia is not null)
            {
                agent.SocialMedia = socialMedia;
            }

            agent.Agency = agency;

            if (language is not null)
            {
                agent.Languages.Add(language);
            }
            return agent;

        }, new { request.AgentId }, splitOn: "twitter,id,name");

        if (agentsDictionary.Count <= 0)
        {
            return Result.Failure<AgentResponse>(AgentErrors.NotFound);
        }

        var agentResponse = agentsDictionary[request.AgentId];

        return agentResponse;
    }
}