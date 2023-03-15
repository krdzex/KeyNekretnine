namespace Shared.RequestFeatures;
public class ReportParameters : RequestParameters
{
    public ReportParameters() => OrderBy = "COUNT(advert_id)";
}
