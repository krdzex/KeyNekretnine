namespace Shared.RequestFeatures;
public class AdminAdvertParameters : RequestParameters
{
    public AdminAdvertParameters() => OrderBy = "created_date";
    public IEnumerable<Int32> AdvertStatusIds { get; set; }
}