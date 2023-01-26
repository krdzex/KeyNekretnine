using Contracts;
using Dapper;
using Repository.RawQuery;
using Shared.DataTransferObjects.AdvertType;

namespace Repository.Repositories;
internal sealed class AdvertTypeRepository : IAdvertTypeRepository
{
    private readonly DapperContext _dapperContext;

    public AdvertTypeRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<IEnumerable<ShowAdvertTypeDto>> GetAdvertTypes(CancellationToken token)
    {
        var getTypesQuery = AdvertTypeQuery.AllAdvertTypesQuery;

        var cmd = new CommandDefinition(getTypesQuery, cancellationToken: token);

        using (var connection = _dapperContext.CreateConnection())
        {
            var advertTypes = await connection.QueryAsync<ShowAdvertTypeDto>(cmd);

            return advertTypes;
        }

    }
};
