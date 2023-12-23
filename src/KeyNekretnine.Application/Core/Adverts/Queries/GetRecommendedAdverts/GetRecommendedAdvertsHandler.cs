using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdverts;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetRecommendedAdverts;
internal sealed class GetRecommendedAdvertsHandler : IQueryHandler<GetRecommendedAdvertsQuery, IReadOnlyList<PagedAdvertResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetRecommendedAdvertsHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }


    public async Task<Result<IReadOnlyList<PagedAdvertResponse>>> Handle(GetRecommendedAdvertsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT * FROM get_recommended_adverts(@referenceId);
            """;

        var cmd = new CommandDefinition(sql, new { request.ReferenceId }, cancellationToken: cancellationToken);

        var recommandedAdverts = await connection.QueryAsync<PagedAdvertResponse>(cmd);

        return recommandedAdverts.ToList();
    }
}