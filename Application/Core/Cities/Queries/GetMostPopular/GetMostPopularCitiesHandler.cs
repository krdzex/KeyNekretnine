using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Cities.Queries.GetMostPopular;
internal sealed class GetMostPopularCitiesHandler : IQueryHandler<GetMostPopularCitiesQuery, IReadOnlyList<PopularCityReponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetMostPopularCitiesHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<PopularCityReponse>>> Handle(GetMostPopularCitiesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
             WITH cte AS (
                    SELECT 
                        c.id,
                        c.name, 
                        c.image_url, 
                        count(a.id) AS adverts_count
                    FROM adverts a
                    INNER JOIN neighborhoods n ON a.neighborhood_id = n.id
                    INNER JOIN cities c ON c.id = n.city_id
                    WHERE a.status_id = 1
                    GROUP BY c.name, c.image_url,c.id
                    )
                    SELECT 
                        id,
                        name, 
                        adverts_count AS AdvertsCount, 
                        image_url AS ImageUrl
                    FROM cte
                    ORDER BY adverts_count DESC
                    LIMIT 5
            """;

        var cmd = new CommandDefinition(sql, cancellationToken: cancellationToken);

        var popularCities = await connection.QueryAsync<PopularCityReponse>(cmd);

        return popularCities.ToList();
    }
}
