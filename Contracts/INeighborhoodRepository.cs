using Shared.DataTransferObjects.Neighborhood;

namespace Contracts;
public interface INeighborhoodRepository
{
    Task<IEnumerable<ShowNeighborhoodDto>> GetNeighborhoods(int id, CancellationToken token);
}
