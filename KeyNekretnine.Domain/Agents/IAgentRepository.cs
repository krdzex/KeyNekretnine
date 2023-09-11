namespace KeyNekretnine.Domain.Agents;
public interface IAgentRepository
{
    Task<Agent?> GetByIdWithAgencyAndLanguagesAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Agent agency);
}