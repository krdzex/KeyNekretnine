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
            .Where(au => au.Id == UpdateId && au.Type == UpdateType)
            .Include(au => au.Advert)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> CanAddUpdate(Guid AdvertId, UpdateTypes UpdateType)
    {
        var update = await DbContext.Set<AdvertUpdate>()
            .Where(au => au.AdvertId == AdvertId
                && au.Type == UpdateType
                && au.ApprovedOnDate == null
                && au.RejectedOnDate == null)
            .FirstOrDefaultAsync();

        if (update is null)
        {
            return true;
        }

        return false;
    }

    public async Task<AdvertUpdate?> GetByIdWithAdvertAndFeatures(Guid UpdateId, UpdateTypes UpdateType, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<AdvertUpdate>()
            .Where(au => au.Id == UpdateId && au.Type == UpdateType)
            .Include(au => au.Advert)
            .ThenInclude(a => a.AdvertFeatures)
            .FirstOrDefaultAsync(cancellationToken);
    }
}