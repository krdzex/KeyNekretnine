using Contracts;
using Dapper;
using Repository.RawQuery;
using Shared.DataTransferObjects.Language;

namespace Repository.Repositories;

internal class LanguageRepository : ILanguageRepository
{
    private readonly DapperContext _dapperContext;
    public LanguageRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<IEnumerable<LanguageDto>> GetAll(CancellationToken cancellationToken)
    {
        var query = LanguageQuery.AllLanguagesQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var cmd = new CommandDefinition(query, cancellationToken: cancellationToken);

            var languages = await connection.QueryAsync<LanguageDto>(cmd);

            return languages;
        }
    }
}