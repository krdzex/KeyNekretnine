using Application.Abstraction.Messaging;
using Contracts;
using Shared.DataTransferObjects.Agency;
using Shared.Error;

namespace Application.Core.Agents.Queries.GetAgentById;
internal sealed class GetAgentByIdHandler : IQueryHandler<GetAgentByIdQuery, AllAgentInformationsDto>
{
    private readonly IRepositoryManager _repository;

    public GetAgentByIdHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<AllAgentInformationsDto>> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
    {
        var agent = await _repository.Agent.GetAgentById(request.AgentId, cancellationToken);

        return agent;
    }
}