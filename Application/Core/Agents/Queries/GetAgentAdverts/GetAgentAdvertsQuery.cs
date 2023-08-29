using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgentAdverts;
public sealed record GetAgentAdvertsQuery(int AgentId) : IQuery<List<MinimalInformationsAboutAdvertDto>>;