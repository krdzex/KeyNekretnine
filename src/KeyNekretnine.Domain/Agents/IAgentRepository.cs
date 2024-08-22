namespace KeyNekretnine.Domain.Agents;
public interface IAgentRepository
{
    Task<Agent?> GetByIdWithAgencyAndLanguagesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Agent?> GetByIdWithAgencyAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Agent agency);
    Task<bool> IsAgentInLoggedAgency(Guid? agentId, string agencyOwnerId, CancellationToken cancellationToken = default);
}