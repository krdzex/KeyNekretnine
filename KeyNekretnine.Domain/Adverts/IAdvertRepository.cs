namespace KeyNekretnine.Domain.Adverts;
public interface IAdvertRepository
{
    Task<Advert?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Advert?> GetByReferenceIdAsync(string ReferenceId, CancellationToken cancellationToken = default);
    void Add(Advert advert);
}