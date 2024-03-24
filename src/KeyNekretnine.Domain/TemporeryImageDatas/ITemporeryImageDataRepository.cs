using Microsoft.AspNetCore.Http;

namespace KeyNekretnine.Domain.TemporeryImageDatas;
public interface ITemporeryImageDataRepository
{
    Task<Guid> Insert(IFormFile image, DateTime dateTime, CancellationToken cancellationToken);
    Task<TemporeryImageData?> GetById(Guid id, CancellationToken cancellationToken);
    Task BulkUpdate(List<Guid> ids, Guid? advertId, CancellationToken cancellationToken);
}