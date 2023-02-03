using Microsoft.AspNetCore.Http;

namespace Contracts
{
    public interface ITemporeryImageDataRepository
    {
        Task<IEnumerable<byte[]>> Get(int advertId, bool isCover);
        Task DeleteAll(int advertId);
        Task Insert(IFormFile image, int advertId, bool is_cover);

    }
}
