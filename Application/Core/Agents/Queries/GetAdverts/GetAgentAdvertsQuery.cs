using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAdverts;
public sealed record GetAgentAdvertsQuery(Guid AgentId) : IQuery<IReadOnlyList<AgentAdvertResponse>>;