//using Shared.CustomResponses;
//using Shared.DataTransferObjects.Advert;
//using Shared.DataTransferObjects.Agency;
//using Shared.RequestFeatures;

//namespace Contracts;
//public interface IAgentRepository
//{
//    Task<int> CreateAgentAndReturnId(CreateAgentDto createAgentDto, CancellationToken cancellationToken);
//    Task<Pagination<MinimalAgentInformationsDto>> GetAgents(AgentParameters agentParameters, CancellationToken cancellationToken);
//    Task<IEnumerable<MinimalInformationsAboutAdvertDto>> GetAgentAdverts(int agentId, CancellationToken cancellationToken);
//    Task<AllAgentInformationsDto> GetAgentById(int agentId, CancellationToken cancellationToken);
//    Task AddLanguageToAgent(int languageId, int agentId, CancellationToken cancellationToken);
//    Task DeleteLanguagesForAgent(int agentId, CancellationToken cancellationToken);
//    Task<string> GetAgentImage(int agentId, CancellationToken cancellationToken);
//    Task UpdateAgent(UpdateAgentDto updateAgentDto, int agentId, CancellationToken cancellationToken);
//    Task<bool> DoesAgentExist(int agentId, CancellationToken cancellationToken);
//    Task<int> AgencyIdOfAgent(int agentId, CancellationToken cancellationToken);
//}