using Shared.DataTransferObjects.AdvertStatus;

namespace Contracts;
public interface IAdvertStatusRepository
{
    Task<IEnumerable<AdvertStatusDto>> GetAdvertsStatuses(CancellationToken token);

}
