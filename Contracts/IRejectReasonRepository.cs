using Shared.DataTransferObjects.RejectReason;

namespace Contracts;
public interface IRejectReasonRepository
{
    Task<IEnumerable<RejectReasonDto>> GetRejectReasons(CancellationToken cancellationToken);
}