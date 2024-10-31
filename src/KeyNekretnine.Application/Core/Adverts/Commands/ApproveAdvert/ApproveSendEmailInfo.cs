namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveAdvert;
public class ApproveSendEmailInfo
{
    public double Price { get; set; }
    public double FloorSpace { get; set; }
    public int NoOfBedrooms { get; set; }
    public int NoOfBathrooms { get; set; }
    public string CoverImageUrl { get; set; }
    public string CityAndNeighborhood { get; set; }
    public string Address { get; set; }
    public int Purpose { get; set; }
    public string CreatorEmail { get; set; }
    public string ReferenceId { get; set; }
}
