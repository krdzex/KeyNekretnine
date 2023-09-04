using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Cities.Queries.Get;
internal sealed class GetCitiesHandler : IQueryHandler<GetCitiesQuery, IReadOnlyList<CityReponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetCitiesHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<CityReponse>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id AS Id,
                name AS Name,
                geo_id AS GeoId
            FROM cities
            """;

        var cmd = new CommandDefinition(sql, cancellationToken: cancellationToken);

        var cities = await connection.QueryAsync<CityReponse>(cmd);

        return cities.ToList();
    }
}