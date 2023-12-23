using KeyNekretnine.Domain.Adverts;
using Microsoft.EntityFrameworkCore;

namespace KeyNekretnine.Infrastructure.Repositories;

internal sealed class AdvertRepository : Repository<Advert>, IAdvertRepository
{
    public AdvertRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Advert?> GetByReferenceIdAsync(
    string referenceId,
    CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Advert>()
            .FirstOrDefaultAsync(advert => advert.ReferenceId == referenceId && advert.Status != AdvertStatus.Uploading, cancellationToken);
    }

    public async Task<Advert?> GetAcceptedAdvertByReferenceIdAsync(
    string referenceId,
    CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Advert>()
            .FirstOrDefaultAsync(advert => advert.ReferenceId == referenceId && advert.Status == AdvertStatus.Accepted, cancellationToken);
    }

    public async Task<Advert?> GetByReferenceIdWithReportsAsync(
    string referenceId,
    CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Advert>()
            .Include(advert => advert.UserAdvertReports)
            .FirstOrDefaultAsync(advert => advert.ReferenceId == referenceId && advert.Status == AdvertStatus.Accepted, cancellationToken);
    }

    public async Task<Advert?> GetByReferenceIdWithUserAsync(string referenceId, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Advert>()
            .Include(advert => advert.User)
            .FirstOrDefaultAsync(advert => advert.ReferenceId == referenceId && advert.Status == AdvertStatus.Accepted, cancellationToken);
    }
}