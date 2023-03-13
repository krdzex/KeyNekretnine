using Shared.DataTransferObjects.AdvertPurpose;

namespace Contracts;
public interface IAdvertPurposeRepository
{
    Task<IEnumerable<AdvertPurposeDto>> GetAdvertPurposes(CancellationToken token);
}