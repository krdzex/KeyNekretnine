namespace Entities.Models;
public class TemporeryImageData : EntityBase
{
    public byte[] ImageData { get; set; }

    public Advert Advert { get; set; }

    public int AdvertId { get; set; }
    public bool IsCover { get; set; }
}
