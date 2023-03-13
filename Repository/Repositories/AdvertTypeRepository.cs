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

    public async Task<IEnumerable<AdvertTypeDto>> GetAdvertTypes(CancellationToken cancellationToken)
    {
        var query = AdvertTypeQuery.AllAdvertTypesQuery;

        using (var connection = _dapperContext.CreateConnection())
        {
            var cmd = new CommandDefinition(query, cancellationToken: cancellationToken);

            var advertTypes = await connection.QueryAsync<AdvertTypeDto>(cmd);

            return advertTypes;
        }

    }
};