using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Agency;

namespace Application.Core.Agencies.Queries.GetAgencyAgents;
public sealed record GetAgencyAgentsQuery(int AgencyId) : IQuery<List<AgentForAgencyDto>>;