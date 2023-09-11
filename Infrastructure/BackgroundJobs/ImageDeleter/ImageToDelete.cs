namespace KeyNekretnine.Infrastructure.BackgroundJobs.ImageDeleter;
public sealed class ImageToDelete
{
    public ImageToDelete(Guid id, DateTime addedOnTime, string imageUrl)
    {
        Id = id;
        AddedOnTime = addedOnTime;
        ImageUrl = imageUrl;
    }

    public Guid Id { get; private set; }
    public DateTime AddedOnTime { get; private set; }
    public string ImageUrl { get; private set; }
    public DateTime? DeletedOnTime { get; private set; }
    public string? Error { get; private set; }
}