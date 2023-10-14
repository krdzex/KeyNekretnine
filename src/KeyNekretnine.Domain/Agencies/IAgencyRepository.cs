namespace KeyNekretnine.Domain.Agencies;
public interface IAgencyRepository
{
    Task<Agency?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Agency?> GetByIdWithLanguagesAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Agency agency);
}