using Contracts;
using Dapper;
using Repository.RawQuery;
using Shared.DataTransferObjects.City;

namespace Repository.Repositories;
internal sealed class CityRepository : ICityRepository
{
    private readonly DapperContext _dapperContext;
    public CityRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<IEnumerable<ShowCityDto>> GetCities(CancellationToken token)
    {
        var getCitiesQuery = CityQuery.AllCities;

        var cmd = new CommandDefinition(getCitiesQuery, cancellationToken: token);

        using (var connection = _dapperContext.CreateConnection())
        {
            var cities = await connection.QueryAsync<ShowCityDto>(cmd);

            return cities;
        }

    }
}

