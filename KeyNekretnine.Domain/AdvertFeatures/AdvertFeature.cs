using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Domain.AdvertFeatures;
public class AdvertFeature : Entity
{
    public string Name { get; set; }
    public Guid AdvertId { get; set; }
}