using Contracts;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Repository.RawQuery;
using Shared.DataTransferObjects.Neighborhood;

namespace Repository.Repositories;
internal sealed class NeighborhoodRepository : INeighborhoodRepository
{
    private readonly RepositoryContext _context;
    public NeighborhoodRepository(RepositoryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ShowNeighborhoodDto>> GetNeighborhoods(int id, CancellationToken token)
    {
        var getNeighborhoodForCity = NeighborhoodQuery.NeighborhoodForCity;

        var cmd = new CommandDefinition(getNeighborhoodForCity, new { id }, cancellationToken: token);

        var neighborhoods = await _context
            .Database
            .GetDbConnection()
            .QueryAsync<ShowNeighborhoodDto>(cmd);

        return neighborhoods;
    }
}
