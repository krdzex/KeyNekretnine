namespace KeyNekretnine.Application.Core.Adverts.Queries.GetLocationUpdate;
public class LocationUpdateResponse
{
    public Guid Id { get; set; }
    public LocationAdvertInformations CurrentValues { get; set; }
    public LocationAdvertInformations NewValues { get; set; }
    public DateTime? ApprovedOnDate { get; set; }
    public DateTime? RejectedOnDate { get; set; }

}

public class LocationAdvertInformations
{
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public string Address { get; init; }
    public int NeighborhoodId { get; init; }
}