using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetById;
public sealed record GetAgentByIdQuery(Guid AgentId) : IQuery<AgentResponse>;