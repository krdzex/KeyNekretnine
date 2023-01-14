using Contracts;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Repository.RawQuery;
using Shared.DataTransferObjects.City;

namespace Repository.Repositories;
internal sealed class CityRepository : ICityRepository
{
    private readonly RepositoryContext _context;
    public CityRepository(RepositoryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ShowCityDto>> GetCities(CancellationToken token)
    {
        var getCitiesQuery = CityQuery.AllCities;

        var cmd = new CommandDefinition(getCitiesQuery, cancellationToken: token);

        var cities = await _context
            .Database
            .GetDbConnection()
            .QueryAsync<ShowCityDto>(cmd);

        return cities;
    }
}

