using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Neighborhoods.Queries.GetNeighborhoodsByCityId;
internal sealed class GetNeighborhoodsByCityIdHandler : IQueryHandler<GetNeighborhoodsByCityIdQuery, IReadOnlyList<NeighborhoodResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetNeighborhoodsByCityIdHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<NeighborhoodResponse>>> Handle(GetNeighborhoodsByCityIdQuery request, CancellationToken cancellationToken)
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