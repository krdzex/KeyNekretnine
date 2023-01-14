using Shared.DataTransferObjects.AdvertType;

namespace Contracts;
public interface IAdvertTypeRepository
{
    Task<IEnumerable<ShowAdvertTypeDto>> GetAdvertTypes(CancellationToken token);
}