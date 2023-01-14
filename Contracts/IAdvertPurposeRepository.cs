using Shared.DataTransferObjects.AdvertPurpose;

namespace Contracts;
public interface IAdvertPurposeRepository
{
    Task<IEnumerable<ShowAdvertPurposeDto>> GetAdvertPurposes(CancellationToken token);
}
