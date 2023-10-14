using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Adverts;
public class MyAdvertsPaginationParameters : RequestParameters
{
    public MyAdvertsPaginationParameters() => OrderBy = "createdOnDate";
    public int? Type { get; set; }
    public int? Purpose { get; set; }
    public int? Status { get; set; }
}
