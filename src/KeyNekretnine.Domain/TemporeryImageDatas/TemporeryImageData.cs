using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Domain.TemporeryImageDatas;
public class TemporeryImageData : Entity
{
    public Guid? AdvertId { get; set; }
    public Guid? UpdateId { get; set; }
    public byte[] ImageData { get; set; }
    public bool IsCover { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsForUpdate { get; set; }
}