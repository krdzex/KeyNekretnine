using Contracts;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Agency;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgentById;
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