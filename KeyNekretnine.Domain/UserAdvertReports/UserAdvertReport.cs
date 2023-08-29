using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.RejectReasons;
using KeyNekretnine.Domain.Users;

namespace KeyNekretnine.Domain.UserAdvertReports;
public class UserAdvertReport
{
    public string UserId { get; set; }
    public User User { get; set; }

    public int AdvertId { get; set; }
    public Advert Advert { get; set; }

    public int RejectReasonId { get; set; }
    public RejectReason RejectReason { get; set; }

    public DateTime CreatedReportDate { get; set; }
}