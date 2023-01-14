using Contracts;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Repository.RawQuery;
using Shared.DataTransferObjects.AdvertType;

namespace Repository.Repositories;
internal sealed class AdvertTypeRepository : IAdvertTypeRepository
{
    private readonly RepositoryContext _context;
    public AdvertTypeRepository(RepositoryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ShowAdvertTypeDto>> GetAdvertTypes(CancellationToken token)
    {
        var getTypesQuery = AdvertTypeQuery.AllAdvertTypesQuery;

        var cmd = new CommandDefinition(getTypesQuery, cancellationToken: token);

        var advertTypes = await _context
            .Database
            .GetDbConnection()
            .QueryAsync<ShowAdvertTypeDto>(cmd);

        return advertTypes;
    }
};
