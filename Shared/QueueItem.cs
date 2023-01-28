namespace Shared;
public class QueueItem
{
    public int AdvertId { get; set; }

    public string CoverImagePath { get; set; }
    public IEnumerable<string> ImagePaths { get; set; }
}
