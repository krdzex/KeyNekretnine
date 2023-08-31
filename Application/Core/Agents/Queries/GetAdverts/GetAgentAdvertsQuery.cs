using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Agents.Queries.GetAdverts;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgentAdverts;
public sealed record GetAgentAdvertsQuery(int AgentId) : IQuery<IReadOnlyList<AgentAdvertResponse>>;