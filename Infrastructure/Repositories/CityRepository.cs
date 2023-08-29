//using Contracts;
//using Dapper;
//using Repository.RawQuery;
//using Shared.DataTransferObjects.City;

//namespace KeyNekretnine.Infrastructure.Repositories;
//internal sealed class CityRepository : ICityRepository
//{
//    private readonly DapperContext _dapperContext;
//    public CityRepository(DapperContext dapperContext)
//    {
//        _dapperContext = dapperContext;
//    }

//    public async Task<IEnumerable<CityDto>> GetCities(CancellationToken cancellationToken)
//    {
//        var query = CityQuery.AllCities;

//        using (var connection = _dapperContext.CreateConnection())
//        {
//            var cmd = new CommandDefinition(query, cancellationToken: cancellationToken);

//            var cities = await connection.QueryAsync<CityDto>(cmd);

//            return cities;
//        }
//    }

//    public async Task<IEnumerable<PopularCitiesDto>> GetMostPopularCities(CancellationToken cancellationToken)
//    {
//        var query = CityQuery.CitiesWithMostAdverts;

//        using (var connection = _dapperContext.CreateConnection())
//        {
//            var cmd = new CommandDefinition(query, cancellationToken: cancellationToken);

//            var cities = await connection.QueryAsync<PopularCitiesDto>(cmd);

//            return cities;
//        }
//    }
//}