
using KeyNekretnine.Domain.TemporeryImageDatas;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IO;

namespace KeyNekretnine.Infrastructure.Repositories;
internal class TemporeryImageDataRepository : ITemporeryImageDataRepository
{
    private readonly RecyclableMemoryStreamManager _memoryManager;
    public readonly ApplicationDbContext _dbContext;
    public TemporeryImageDataRepository(ApplicationDbContext dbContext, RecyclableMemoryStreamManager memoryManager)
    {
        _dbContext = dbContext;
        _memoryManager = memoryManager;
    }

    public async Task BulkUpdateForCreatingAdvert(List<Guid> ids, Guid? advertId, CancellationToken cancellationToken)
    {
        await _dbContext.TemporeryImagesData
            .Where(x => ids.Contains(x.Id))
            .ExecuteUpdateAsync(x => x.SetProperty(
                x => x.AdvertId, advertId), cancellationToken: cancellationToken);
    }

    public async Task BulkUpdateForUpdatingAdvert(List<Guid> ids, Guid? updateId, CancellationToken cancellationToken)
    {
        await _dbContext.TemporeryImagesData
            .Where(x => ids.Contains(x.Id))
            .ExecuteUpdateAsync(x => x.SetProperty(
                x => x.UpdateId, updateId), cancellationToken: cancellationToken);
    }

    public async Task<TemporeryImageData?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var img = await _dbContext.TemporeryImagesData.Where(x => x.Id == id).FirstOrDefaultAsync();

        return img;
    }

    public async Task<Guid> Insert(IFormFile image, DateTime dateTime, CancellationToken cancellationToken)
    {
        using var memoryStream = _memoryManager.GetStream("InsertImageStream");

        await image.CopyToAsync(memoryStream, cancellationToken);
        memoryStream.Position = 0;

        byte[] internalBuffer = memoryStream.GetBuffer();
        int dataLength = (int)memoryStream.Length;

        byte[] buffer = new byte[dataLength];
        Buffer.BlockCopy(internalBuffer, 0, buffer, 0, dataLength);

        var newImage = new TemporeryImageData
        {
            Id = Guid.NewGuid(),
            ImageData = buffer,
            IsCover = false,
            CreatedDate = dateTime
        };

        _dbContext.TemporeryImagesData.Add(newImage);

        return newImage.Id;
    }
}