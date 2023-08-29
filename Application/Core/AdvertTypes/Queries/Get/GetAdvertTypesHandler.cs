using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.AdvertTypes.Queries.GetAdvertTypes;
internal sealed class GetAdvertTypesHandler : IQueryHandler<GetAdvertTypesQuery, IReadOnlyList<AdvertTypeResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAdvertTypesHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<AdvertTypeResponse>>> Handle(GetAdvertTypesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id AS Id,
                name_sr AS NameSr,
                name_en AS NameEn
            FROM advert_types
            """;

        var cmd = new CommandDefinition(sql, cancellationToken: cancellationToken);

        var advertTypes = await connection.QueryAsync<AdvertTypeResponse>(cmd);

        return advertTypes.ToList();
    }
}