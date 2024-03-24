using KeyNekretnine.Domain.AdvertUpdates;
using Microsoft.EntityFrameworkCore;

namespace KeyNekretnine.Infrastructure.Repositories;
internal sealed class AdvertUpdateRepository : Repository<AdvertUpdate>, IAdvertUpdateRepository
{
    public AdvertUpdateRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<AdvertUpdate?> GetByIdWithAdvert(Guid UpdateId, UpdateTypes UpdateType, CancellationToken cancellationToken)
    {
        return await DbContext.Set<AdvertUpdate>()
            .Include(au => au.Advert)
            .Where(au => au.Id == UpdateId && au.Type == UpdateType && au.ApprovedOnDate == null && au.RejectedOnDate == null)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> CanAddUpdate(Guid AdvertId, UpdateTypes UpdateType)
    {
        var updatesForTypeAndReferenceId = await DbContext.Set<AdvertUpdate>().Where(au => au.AdvertId == AdvertId && au.Type == UpdateType).ToListAsync();

        if (!updatesForTypeAndReferenceId.Any())
        {
            return true;
        }

        var updatesWithNullDates = updatesForTypeAndReferenceId.Where(au => au.ApprovedOnDate == null && au.RejectedOnDate == null);

        if (updatesWithNullDates.Any())
        {
            return false;
        }

        return true;
    }
}