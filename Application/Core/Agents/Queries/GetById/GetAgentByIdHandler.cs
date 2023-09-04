using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Language.Queries.Get;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Agents;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetById;
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

        Dictionary<int, AgentResponse> agentsDictionary = new();

        const string sql = """
            SELECT
                a.id,
                a.first_name AS firstName,
                a.last_Name AS lastName,
                a.email,
                a.image_Url AS imageUrl,
                a.description,
                a.phone_number AS phoneNumber,
                a.twitter_Url AS twitter,
                a.facebook_Url AS facebook,
                a.instagram_Url AS instagram,
                a.linkedin_Url AS linkedin,
                a.linkedin_Url AS linkedin,
                ag.id AS agencyId,
                ag.name AS agencyName,
                l.id AS languageId,
                l.name
            FROM agents AS a
            LEFT JOIN agencies as ag ON ag.id = a.agency_id
            LEFT JOIN agent_languages al ON al.agent_id = a.id
            LEFT JOIN languages l ON al.language_id = l.id
            WHERE a.id = @AgentId;
            """;

        var agent = await connection.QueryAsync<AgentResponse, SocialMediaResponse, ShortAgencyResponse, LanguageResponse, AgentResponse>(
            sql,
            (agent, socialNetwork, agency, language) =>
        {
            if (agentsDictionary.TryGetValue(agent.Id, out var existingAgent))
            {
                agent = existingAgent;
            }
            else
            {
                agentsDictionary.Add(agent.Id, agent);
            }
            agent.SocialNetwork ??= socialNetwork;
            agent.Agency = agency;

            if (language is not null)
            {
                agent.Languages.Add(language);
            }
            return agent;

        }, new { request.AgentId }, splitOn: "twitter,agencyId,languageId");

        if (agentsDictionary.Count <= 0)
        {
            return Result.Failure<AgentResponse>(AgentErrors.NotFound);
        }

        var agentResponse = agentsDictionary[request.AgentId];

        return agentResponse;
    }
}