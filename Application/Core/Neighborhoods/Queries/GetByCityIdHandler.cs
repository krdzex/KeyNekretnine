using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Neighborhoods.Queries;
internal sealed class GetByCityIdHandler : IQueryHandler<GetByCityIdQuery, IReadOnlyList<NeighborhoodResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetByCityIdHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<NeighborhoodResponse>>> Handle(GetByCityIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id AS Id,
                name AS Name
            FROM neighborhoods
            WHERE city_id = @CityId
            """;

        var cmd = new CommandDefinition(sql, new { request.CityId }, cancellationToken: cancellationToken);

        var neighborhoods = await connection.QueryAsync<NeighborhoodResponse>(cmd);

        return neighborhoods.ToList();
    }
}