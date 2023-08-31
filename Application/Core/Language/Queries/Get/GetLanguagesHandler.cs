using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Language.Queries.Get;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Language.Queries.GetAllLanguages;
internal sealed class GetLanguagesHandler : IQueryHandler<GetLanguagesQuery, IReadOnlyList<LanguageResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetLanguagesHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<LanguageResponse>>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id AS Id,
                name AS Name
            FROM languages
            """;

        var cmd = new CommandDefinition(sql, cancellationToken: cancellationToken);

        var languages = await connection.QueryAsync<LanguageResponse>(cmd);

        return languages.ToList();
    }
}