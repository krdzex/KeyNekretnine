using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.DataTransferObjects.Agency;
using Shared.RequestFeatures;

namespace Contracts;
public interface IAgentRepository
{
    Task CreateAgent(CreateAgentDto createAgentDto, CancellationToken cancellationToken);
    Task<Pagination<MinimalAgentInformationsDto>> GetAgents(AgentParameters agentParameters, CancellationToken cancellationToken);
    Task<IEnumerable<MinimalInformationsAboutAdvertDto>> GetAgentAdverts(int agentId, CancellationToken cancellationToken);
    Task<AllAgentInformationsDto> GetAgentById(int agentId, CancellationToken cancellationToken);
}
