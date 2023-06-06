using Shared.DataTransferObjects.Neighborhood;

namespace Contracts;
public interface INeighborhoodRepository
{
    Task<IEnumerable<NeighborhoodDto>> GetNeighborhoods(int city, CancellationToken token);
}