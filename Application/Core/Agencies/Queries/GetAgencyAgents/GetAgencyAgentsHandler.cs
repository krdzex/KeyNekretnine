using Application.Abstraction.Messaging;
using Contracts;
using Shared.DataTransferObjects.Agency;
using Shared.Error;

namespace Application.Core.Agencies.Queries.GetAgencyAgents;
internal sealed class GetAgencyAgentsHandler : IQueryHandler<GetAgencyAgentsQuery, List<AgentForAgencyDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAgencyAgentsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<AgentForAgencyDto>>> Handle(GetAgencyAgentsQuery request, CancellationToken cancellationToken)
    {
        var agents = await _repository.Agency.GetAgents(request.AgencyId, cancellationToken);

        return agents.ToList();
    }
}