using Contracts;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Repository.RawQuery;
using Shared.DataTransferObjects.AdvertPurpose;

namespace Repository.Repositories;
internal sealed class AdvertPurposeRepository : IAdvertPurposeRepository
{
    private readonly RepositoryContext _context;
    public AdvertPurposeRepository(RepositoryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ShowAdvertPurposeDto>> GetAdvertPurposes(CancellationToken token)
    {
        var getPurposesQuery = AdvertPurposeQuery.AllAdvertPurposesQuery;

        var cmd = new CommandDefinition(getPurposesQuery, cancellationToken: token);

        var advertPurposes = await _context
            .Database
            .GetDbConnection()
            .QueryAsync<ShowAdvertPurposeDto>(cmd);

        return advertPurposes;
    }
};
