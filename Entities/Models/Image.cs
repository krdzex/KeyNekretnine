namespace Entities.Models;
public class Image : EntityBase
{
    public string Url { get; set; }
    public int AdvertId { get; set; }
    public string PublicId { get; set; }
    public Advert Advert { get; set; }
}