using Shared.DataTransferObjects.City;

namespace Contracts;
public interface ICityRepository
{
    Task<IEnumerable<ShowCityDto>> GetCities(CancellationToken token);
    Task<IEnumerable<PopularCitiesDto>> GetMostPopularCities(CancellationToken token);
}

