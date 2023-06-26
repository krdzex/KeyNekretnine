using Shared.DataTransferObjects.Agency;

namespace Contracts;
public interface IAgentRepository
{
    Task CreateAgent(CreateAgentDto createAgentDto, CancellationToken cancellationToken);

}
