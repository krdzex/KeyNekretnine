namespace Entities.Models;
public class City : EntityBase
{
    public string Name { get; set; }
    public string GeoId { get; set; }
    public List<Neighborhood> Neighborhoods { get; set; }
}
