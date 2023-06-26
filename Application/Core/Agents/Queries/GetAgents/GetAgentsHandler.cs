using Application.Abstraction.Messaging;
using Contracts;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Agency;
using Shared.Error;

namespace Application.Core.Agents.Queries.GetAgents;
internal sealed class GetAgentsHandler : IQueryHandler<GetAgentsQuery, Pagination<MinimalAgentInformationsDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAgentsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<Pagination<MinimalAgentInformationsDto>>> Handle(GetAgentsQuery request, CancellationToken cancellationToken)
    {
        //var agents = await _repository.Agency.GetAgencyLocation(request.AgencyId, cancellationToken);

        //return agents;

        return new Pagination<MinimalAgentInformationsDto>();
    }
}