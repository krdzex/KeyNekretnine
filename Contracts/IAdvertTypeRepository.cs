using Shared.DataTransferObjects.AdvertType;

namespace Contracts;
public interface IAdvertTypeRepository
{
    Task<IEnumerable<AdvertTypeDto>> GetAdvertTypes(CancellationToken token);
}