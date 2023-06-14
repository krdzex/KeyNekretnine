namespace Shared.RequestFeatures;
public class AgencyParameters : RequestParameters
{
    public AgencyParameters() => OrderBy = "created_date";
    public string? Name { get; set; }
}
