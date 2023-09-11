using KeyNekretnine.Domain.Models;

namespace KeyNekretnine.Domain.TemporeryImageDatas;
public class TemporeryImageData : EntityBase
{
    public Guid AdvertId { get; set; }
    public byte[] ImageData { get; set; }
    public bool IsCover { get; set; }
}