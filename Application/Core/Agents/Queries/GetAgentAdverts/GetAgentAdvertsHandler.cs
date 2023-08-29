using Contracts;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgentAdverts;
internal sealed class GetAgentAdvertsHandler : IQueryHandler<GetAgentAdvertsQuery, List<MinimalInformationsAboutAdvertDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAgentAdvertsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<MinimalInformationsAboutAdvertDto>>> Handle(GetAgentAdvertsQuery request, CancellationToken cancellationToken)
    {
        var adverts = await _repository.Agent.GetAgentAdverts(request.AgentId, cancellationToken);

        return adverts.ToList();
    }
}