using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Agency;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgentById;
public sealed record GetAgentByIdQuery(int AgentId) : IQuery<AllAgentInformationsDto>;