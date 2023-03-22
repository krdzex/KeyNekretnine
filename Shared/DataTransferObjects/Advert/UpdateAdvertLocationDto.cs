namespace Shared.DataTransferObjects.Advert;
public class UpdateAdvertLocationDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Street { get; set; }
    public int NeighborhoodId { get; set; }

}
