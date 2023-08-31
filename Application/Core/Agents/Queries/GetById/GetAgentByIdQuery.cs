using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Agents.Queries.GetById;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgentById;
public sealed record GetAgentByIdQuery(int AgentId) : IQuery<AgentResponse>;