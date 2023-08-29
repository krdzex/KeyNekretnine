using KeyNekretnine.Domain.Models;
using KeyNekretnine.Domain.UserAdvertReports;

namespace KeyNekretnine.Domain.RejectReasons;
public class RejectReason : EntityBase
{
    public string ReasonSr { get; set; }
    public string ReasonEn { get; set; }
    public IEnumerable<UserAdvertReport> UserAdvertReports { get; set; }
}