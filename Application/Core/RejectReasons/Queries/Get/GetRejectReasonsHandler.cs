using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.RejectReasons.Queries.Get;
internal sealed class GetRejectReasonsHandler : IQueryHandler<GetRejectReasonsQuery, IReadOnlyList<RejectReasonResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetRejectReasonsHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<RejectReasonResponse>>> Handle(GetRejectReasonsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id,
                reason_sr AS reasonSr,
                reason_en AS reasonEn
            FROM reject_reasons
            """;

        var cmd = new CommandDefinition(sql, cancellationToken: cancellationToken);

        var rejectReasons = await connection.QueryAsync<RejectReasonResponse>(cmd);

        return rejectReasons.ToList();
    }
}