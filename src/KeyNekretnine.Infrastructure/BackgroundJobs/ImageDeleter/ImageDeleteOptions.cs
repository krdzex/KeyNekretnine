namespace KeyNekretnine.Infrastructure.BackgroundJobs.ImageDeleter;
public sealed class ImageDeleteOptions
{
    public int IntervalInSeconds { get; init; }

    public int BatchSize { get; init; }
}
