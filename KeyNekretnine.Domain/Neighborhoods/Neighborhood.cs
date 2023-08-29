using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Cities;
using KeyNekretnine.Domain.Models;

namespace KeyNekretnine.Domain.Neighborhoods;
public class Neighborhood : EntityBase
{
    public string Name { get; set; }

    public City City { get; set; }
    public int CityId { get; set; }

    public List<Advert> Adverts { get; set; }
}