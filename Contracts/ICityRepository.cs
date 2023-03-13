using Shared.DataTransferObjects.City;

namespace Contracts;
public interface ICityRepository
{
    Task<IEnumerable<CityDto>> GetCities(CancellationToken token);
    Task<IEnumerable<PopularCitiesDto>> GetMostPopularCities(CancellationToken token);
}