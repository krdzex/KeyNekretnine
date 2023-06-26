using Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Agency;

namespace Application.Core.Agents.Queries.GetAgents;
public sealed record GetAgentsQuery() : IQuery<Pagination<MinimalAgentInformationsDto>>;