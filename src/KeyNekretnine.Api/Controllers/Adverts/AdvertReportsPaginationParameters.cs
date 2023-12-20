using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Adverts;
public record AdvertReportsPaginationParameters : RequestParameters
{
    public AdvertReportsPaginationParameters() => OrderBy = "allReports";
}
