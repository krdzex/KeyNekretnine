namespace Entities.Models;
public class AdvertFeature : EntityBase
{
    public string Name { get; set; }
    public int AdvertId { get; set; }
    public Advert Advert { get; set; }
}