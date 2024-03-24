
using KeyNekretnine.Domain.TemporeryImageDatas;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IO;

namespace KeyNekretnine.Infrastructure.Repositories;
internal class TemporeryImageDataRepository : ITemporeryImageDataRepository
{
    private static readonly RecyclableMemoryStreamManager manager = new RecyclableMemoryStreamManager(new RecyclableMemoryStreamManager.Options()
    {
        BlockSize = 1024,
        LargeBufferMultiple = 1024 * 1024,
        MaximumBufferSize = 16 * 1024 * 1024,
        GenerateCallStacks = true,
        AggressiveBufferReturn = true,
        MaximumLargePoolFreeBytes = 16 * 1024 * 1024 * 4,
        MaximumSmallPoolFreeBytes = 100 * 1024,
    });

    public readonly ApplicationDbContext _dbContext;
    public TemporeryImageDataRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task BulkUpdate(List<Guid> ids, Guid? advertId, CancellationToken cancellationToken)
    {
        await _dbContext.TemporeryImagesData
            .Where(x => ids.Contains(x.Id))
            .ExecuteUpdateAsync(x => x.SetProperty(
                x => x.AdvertId, advertId), cancellationToken: cancellationToken);
    }

    public async Task<TemporeryImageData?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var img = await _dbContext.TemporeryImagesData.Where(x => x.Id == id).FirstOrDefaultAsync();

        return img;
    }

    public async Task<Guid> Insert(IFormFile image, DateTime dateTime, CancellationToken cancellationToken)
    {

        using var memoryStream = manager.GetStream("test");

        await image.CopyToAsync(memoryStream, cancellationToken);
        memoryStream.Position = 0;
        var buffer = new byte[memoryStream.Length];
        await memoryStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);

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