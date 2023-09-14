using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Adverts;
public class AdvertReportsPaginationParameters : RequestParameters
{
    public AdvertReportsPaginationParameters() => OrderBy = "allReports";
}
