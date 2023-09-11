namespace KeyNekretnine.Infrastructure.BackgroundJobs.ImageDeleter;
public interface IImageToDeleteRepository
{
    void Add(string imageUrl, DateTime addedOnTime);
}
