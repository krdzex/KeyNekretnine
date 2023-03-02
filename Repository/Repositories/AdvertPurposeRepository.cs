using Contracts;
using Dapper;
using Repository.RawQuery;
using Shared.DataTransferObjects.AdvertPurpose;

namespace Repository.Repositories;
internal sealed class AdvertPurposeRepository : IAdvertPurposeRepository
{
    private readonly DapperContext _dapperContext;
    public AdvertPurposeRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<IEnumerable<ShowAdvertPurposeDto>> GetAdvertPurposes(CancellationToken cancellationToken)
    {
        var query = AdvertPurposeQuery.AllAdvertPurposesQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var cmd = new CommandDefinition(query, cancellationToken: cancellationToken);

            var advertPurposes = await connection.QueryAsync<ShowAdvertPurposeDto>(cmd);

            return advertPurposes;
        }
    }
};