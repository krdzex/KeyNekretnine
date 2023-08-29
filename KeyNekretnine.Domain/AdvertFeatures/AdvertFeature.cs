using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Models;

namespace KeyNekretnine.Domain.AdvertFeatures;
public class AdvertFeature : EntityBase
{
    public string Name { get; set; }
    public int AdvertId { get; set; }
    public Advert Advert { get; set; }
}