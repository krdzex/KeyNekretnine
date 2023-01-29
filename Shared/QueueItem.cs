namespace Shared;
public class QueueItem
{
    public int AdvertId { get; set; }

    public byte[] CoverImageData { get; set; }
    public IEnumerable<byte[]> ImagesData { get; set; }
}
