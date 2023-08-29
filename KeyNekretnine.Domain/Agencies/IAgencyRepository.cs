using Shared.DataTransferObjects.Agency;

namespace KeyNekretnine.Domain.Agencies;
public interface IAgencyRepository
{
    Task CreateAgency(string name, string userId, CancellationToken cancellationToken);
    Task<bool> IsUserAgencyOwner(string userId, int agencyId, CancellationToken cancellationToken);
    Task<bool> DoesAgencyExist(int agencyId, CancellationToken cancellationToken);
    Task<GetAgencyDto> GetAgencyById(int agencyId, CancellationToken cancellationToken);
    Task<string> GetAgencyImage(int agencyId, CancellationToken cancellationToken);
    Task UpdateAgency(UpdateAgencyDto updateAgencyDto, int agencyId, CancellationToken cancellationToken);
    Task AddLanguageToAgency(int languageId, int agencyId, CancellationToken cancellationToken);
    Task DeleteLanguagesForAgency(int agencyId, CancellationToken cancellationToken);
    Task<AgencyLocationDto> GetAgencyLocation(int agencyId, CancellationToken cancellationToken);
}