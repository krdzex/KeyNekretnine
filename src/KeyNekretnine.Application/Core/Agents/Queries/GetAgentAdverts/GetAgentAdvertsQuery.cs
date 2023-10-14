using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgentAdverts;
public sealed record GetAgentAdvertsQuery(Guid AgentId) : IQuery<IReadOnlyList<AgentAdvertResponse>>;