namespace Entities.Models;
public class AdvertFeature
{
    public string Name { get; set; }
    public int AdvertId { get; set; }
    public Advert Advert { get; set; }
}
