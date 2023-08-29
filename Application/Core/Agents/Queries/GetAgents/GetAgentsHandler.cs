using Contracts;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Agency;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgents;
internal sealed class GetAgentsHandler : IQueryHandler<GetAgentsQuery, Pagination<MinimalAgentInformationsDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAgentsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<Pagination<MinimalAgentInformationsDto>>> Handle(GetAgentsQuery request, CancellationToken cancellationToken)
    {
        var agents = await _repository.Agent.GetAgents(request.AgentParameters, cancellationToken);

        return agents;
    }
}