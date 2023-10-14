using KeyNekretnine.Domain.Models;

namespace KeyNekretnine.Domain.Cities;
public class City : EntityBase
{
    public string Name { get; set; }
    public string GeoId { get; set; }
    public string ImageUrl { get; set; }
}