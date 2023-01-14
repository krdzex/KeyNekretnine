namespace Entities.Models;
public class Neighborhood : EntityBase
{
    public string Name { get; set; }
    public City City { get; set; }
    public int CityId { get; set; }
    public List<Advert> Adverts { get; set; }
}
