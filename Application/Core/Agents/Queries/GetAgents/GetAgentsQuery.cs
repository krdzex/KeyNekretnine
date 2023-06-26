using Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Agency;
using Shared.RequestFeatures;

namespace Application.Core.Agents.Queries.GetAgents;
public sealed record GetAgentsQuery(AgentParameters AgentParameters) : IQuery<Pagination<MinimalAgentInformationsDto>>;