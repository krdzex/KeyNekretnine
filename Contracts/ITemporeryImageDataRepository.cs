using Microsoft.AspNetCore.Http;

namespace Contracts;
public interface ITemporeryImageDataRepository
{
    Task<IEnumerable<byte[]>> Get(int advertId, bool isCover, CancellationToken cancellationToken);
    Task DeleteAll(int advertId, CancellationToken cancellationToken);
    Task Insert(IFormFile image, int advertId, bool is_cover, CancellationToken cancellationToken);
}