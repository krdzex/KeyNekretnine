namespace KeyNekretnine.Api.Controllers.Adverts;
public class UpdateAdvertLocationRequest
{
    public string Address { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public int NeighborhoodId { get; set; }
}
