namespace KeyNekretnine.Domain.Adverts;
public interface IAdvertRepository
{
    Task<Advert?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Advert?> GetByReferenceIdAsync(string ReferenceId, CancellationToken cancellationToken = default);
    Task<Advert?> GetByReferenceIdWithImagesAsync(string ReferenceId, CancellationToken cancellationToken = default);
    Task<Advert?> GetByReferenceIdWithReportsAsync(string ReferenceId, CancellationToken cancellationToken = default);
    Task<Advert?> GetByReferenceIdWithFeaturesAsync(string ReferenceId, CancellationToken cancellationToken = default);
    Task<Advert?> GetAcceptedAdvertByReferenceIdAsync(string ReferenceId, CancellationToken cancellationToken = default);
    Task<Advert?> GetByReferenceIdWithUserAsync(string ReferenceId, CancellationToken cancellationToken = default);
    void Add(Advert advert);
}