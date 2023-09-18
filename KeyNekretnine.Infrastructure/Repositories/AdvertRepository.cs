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
            .FirstOrDefaultAsync(advert => advert.ReferenceId == referenceId, cancellationToken);
    }
}