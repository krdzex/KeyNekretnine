namespace KeyNekretnine.Domain.UserAdvertReports;
public class UserAdvertReport
{
    public string UserId { get; set; }
    public Guid AdvertId { get; set; }
    public int RejectReasonId { get; set; }
    public DateTime CreatedReportDate { get; set; }
}