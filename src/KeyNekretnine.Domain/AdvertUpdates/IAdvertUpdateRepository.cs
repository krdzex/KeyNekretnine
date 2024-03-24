namespace KeyNekretnine.Domain.AdvertUpdates;
public interface IAdvertUpdateRepository
{
    Task<bool> CanAddUpdate(Guid AdvertId, UpdateTypes UpdateType);
    Task<AdvertUpdate?> GetByIdWithAdvert(Guid UpdateId, UpdateTypes UpdateType, CancellationToken cancellationToken = default);
    void Add(AdvertUpdate advert);
}