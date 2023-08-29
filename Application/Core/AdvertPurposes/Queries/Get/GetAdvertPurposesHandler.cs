using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.AdvertPurposes.Queries.GetAdvertPurposes;
internal sealed class GetAdvertPurposesHandler : IQueryHandler<GetAdvertPurposesQuery, IReadOnlyList<AdvertPurposeResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAdvertPurposesHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<AdvertPurposeResponse>>> Handle(GetAdvertPurposesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id AS Id,
                name_sr AS NameSr,
                name_en AS NameEn
            FROM advert_purposes
            """;

        var cmd = new CommandDefinition(sql, cancellationToken: cancellationToken);

        var advertPurposes = await connection.QueryAsync<AdvertPurposeResponse>(cmd);

        return advertPurposes.ToList();
    }
}

