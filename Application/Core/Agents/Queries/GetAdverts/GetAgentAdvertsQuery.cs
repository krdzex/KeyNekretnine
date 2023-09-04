using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAdverts;
public sealed record GetAgentAdvertsQuery(int AgentId) : IQuery<IReadOnlyList<AgentAdvertResponse>>;