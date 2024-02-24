namespace KeyNekretnine.Application.Core.Shared;
public class AdvertLocationResponse
{
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int NeighborhoodId { get; set; }
    public string NeighborhoodName { get; set; }
    public string CitySlug { get; set; }
    public string CityName { get; set; }
}
