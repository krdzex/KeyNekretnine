using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Neighborhoods.Queries.GetNeighborhoodsByCitySlug;
internal sealed class GetNeighborhoodsByCitySlugHandler : IQueryHandler<GetNeighborhoodsByCitySlugQuery, IReadOnlyList<NeighborhoodResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetNeighborhoodsByCitySlugHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<NeighborhoodResponse>>> Handle(GetNeighborhoodsByCitySlugQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                n.id AS Id,
                n.name AS Name
            FROM neighborhoods n
            LEFT JOIN cities c ON c.id = n.city_id
            WHERE c.slug = @CitySlug
            """;

        var cmd = new CommandDefinition(sql, new { request.CitySlug }, cancellationToken: cancellationToken);

        var neighborhoods = await connection.QueryAsync<NeighborhoodResponse>(cmd);

        return neighborhoods.ToList();
    }
}