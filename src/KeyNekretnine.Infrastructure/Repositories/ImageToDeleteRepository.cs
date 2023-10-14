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

    public void Add(string imageUrl, DateTime addedOnTime)
    {
        var imagetoDelete = new ImageToDelete(Guid.NewGuid(), addedOnTime, imageUrl);

        _context.Add(imagetoDelete);
    }
}
