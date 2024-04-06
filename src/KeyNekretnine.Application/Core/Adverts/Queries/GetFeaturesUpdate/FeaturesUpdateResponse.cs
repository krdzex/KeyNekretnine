namespace KeyNekretnine.Application.Core.Adverts.Queries.GetFeaturesUpdate;
public class FeaturesUpdateResponse
{
    public Guid Id { get; set; }
    public List<string> CurrentValues { get; set; } = new();
    public FeaturesInformations NewValues { get; set; }
    public DateTime? ApprovedOnDate { get; set; }
    public DateTime? RejectedOnDate { get; set; }

}

public class FeaturesInformations
{
    public List<string> Features { get; set; } = new();
}