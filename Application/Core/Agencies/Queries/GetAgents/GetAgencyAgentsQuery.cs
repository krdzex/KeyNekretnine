using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyAgents;
public sealed record GetAgencyAgentsQuery(int AgencyId) : IQuery<IReadOnlyList<AgencyAgentResponse>>;