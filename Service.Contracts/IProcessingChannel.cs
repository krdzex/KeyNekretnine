using Shared;

namespace Service.Contracts;
public interface IProcessingChannel
{
    Task<bool> AddQueueItemAsync(QueueItem item, CancellationToken ct = default);

    IAsyncEnumerable<QueueItem> ReadAllAsync(CancellationToken ct = default);

    bool TryCompleteWriter(Exception? ex = null);
}
