namespace Entities.Models;
public class TemporeryImageData : EntityBase
{
    public Advert Advert { get; set; }
    public int AdvertId { get; set; }

    public byte[] ImageData { get; set; }
    public bool IsCover { get; set; }
}