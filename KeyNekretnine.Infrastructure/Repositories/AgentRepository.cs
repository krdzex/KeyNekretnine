using KeyNekretnine.Domain.Agents;
using Microsoft.EntityFrameworkCore;

namespace KeyNekretnine.Infrastructure.Repositories;
internal sealed class AgentRepository : Repository<Agent>, IAgentRepository
{
    public AgentRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Agent?> GetByIdWithAgencyAndLanguagesAsync(
    Guid id,
    CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Agent>()
            .Include(agent => agent.Agency)
            .Include(agent => agent.AgentLanguages)
            .FirstOrDefaultAsync(agent => agent.Id == id, cancellationToken);
    }

    public async Task<bool> IsAgentInLoggedAgency(
    Guid? agentId,
    string agencyOwnerId,
    CancellationToken cancellationToken = default)
    {
        return await DbContext
        .Set<Agent>()
        .Include(agent => agent.Agency)
        .AnyAsync(agent => agent.Id == agentId && agent.Agency.UserId == agencyOwnerId, cancellationToken);
    }
}