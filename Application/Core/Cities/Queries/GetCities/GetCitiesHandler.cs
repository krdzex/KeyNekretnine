using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Cities.Queries.GetCities;
internal sealed class GetCitiesHandler : IQueryHandler<GetCitiesQuery, List<CityReponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetCitiesHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<List<CityReponse>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id AS Id,
                name AS Name,
                geo_id AS GeoId,
            FROM cities
            """;

        var cmd = new CommandDefinition(sql, cancellationToken: cancellationToken);

        var cities = await connection.QueryAsync<CityReponse>(cmd);

        return cities.ToList();
    }
}