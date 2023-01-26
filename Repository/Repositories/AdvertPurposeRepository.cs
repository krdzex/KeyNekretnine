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

    public async Task<IEnumerable<ShowAdvertPurposeDto>> GetAdvertPurposes(CancellationToken token)
    {
        var getPurposesQuery = AdvertPurposeQuery.AllAdvertPurposesQuery;

        var cmd = new CommandDefinition(getPurposesQuery, cancellationToken: token);

        using (var connection = _dapperContext.CreateConnection())
        {
            var advertPurposes = await connection.QueryAsync<ShowAdvertPurposeDto>(cmd);

            return advertPurposes;
        }


    }
};
