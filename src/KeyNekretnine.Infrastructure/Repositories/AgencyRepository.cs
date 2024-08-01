using KeyNekretnine.Domain.Agencies;
using Microsoft.EntityFrameworkCore;

namespace KeyNekretnine.Infrastructure.Repositories;
internal sealed class AgencyRepository : Repository<Agency>, IAgencyRepository
{
    public AgencyRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Agency?> GetByIdWithLanguagesAsync(
    Guid id,
    CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Agency>()
            .Include(agency => agency.AgencyLanguages)
            .FirstOrDefaultAsync(agency => agency.Id == id, cancellationToken);
    }

    public async Task<Agency?> GetByOwnerIdAsync(string ownerId, CancellationToken cancellationToken)
    {
        return await DbContext
            .Set<Agency>()
            .FirstOrDefaultAsync(agency => agency.UserId == ownerId, cancellationToken);
    }

    public async Task<Agency?> GetByOwnerIdWithAgentsAsync(string ownerId, CancellationToken cancellationToken)
    {
        return await DbContext
            .Set<Agency>()
            .Include(agency => agency.Agents)
            .FirstOrDefaultAsync(agency => agency.UserId == ownerId, cancellationToken);
    }
}