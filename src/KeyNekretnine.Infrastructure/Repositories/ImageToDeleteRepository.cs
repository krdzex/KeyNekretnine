using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Infrastructure.BackgroundJobs.ImageDeleter;

namespace KeyNekretnine.Infrastructure.Repositories;
internal sealed class ImageToDeleteRepository : IImageToDeleteRepository
{
    private readonly ApplicationDbContext _context;

    public ImageToDeleteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(string imageUrl, DateTime addedOnTime, CancellationToken cancellationToken)
    {
        var imagetoDelete = new ImageToDelete(Guid.NewGuid(), addedOnTime, imageUrl);

        await _context.AddAsync(imagetoDelete, cancellationToken);
    }
}
