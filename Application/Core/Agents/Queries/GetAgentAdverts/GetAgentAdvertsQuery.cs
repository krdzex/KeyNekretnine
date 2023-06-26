using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace Application.Core.Agents.Queries.GetAgentAdverts;
public sealed record GetAgentAdvertsQuery(int AgentId) : IQuery<List<MinimalInformationsAboutAdvertDto>>;