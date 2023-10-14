using KeyNekretnine.Domain.Models;

namespace KeyNekretnine.Domain.Neighborhoods;
public class Neighborhood : EntityBase
{
    public string Name { get; set; }
    public int CityId { get; set; }
}