using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgents;
public sealed record GetAgencyAgentsQuery(int AgencyId) : IQuery<IReadOnlyList<AgencyAgentResponse>>;