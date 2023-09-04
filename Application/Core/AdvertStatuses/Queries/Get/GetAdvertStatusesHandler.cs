using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.AdvertStatuses.Queries.Get;
internal sealed class GetAdvertStatusesHandler : IQueryHandler<GetAdvertStatusesQuery, IReadOnlyList<AdvertStatusResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAdvertStatusesHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<AdvertStatusResponse>>> Handle(GetAdvertStatusesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id AS Id,
                name_sr AS NameSr,
                name_en AS NameEn
            FROM advert_statuses
            WHERE id != 4;
            """;

        var cmd = new CommandDefinition(sql, cancellationToken: cancellationToken);

        var advertStatuses = await connection.QueryAsync<AdvertStatusResponse>(cmd);

        return advertStatuses.ToList();
    }
}