namespace Entities.Models;
public class RejectionReason : EntityBase
{
    public string ReasonSr { get; set; }
    public string ReasonEn { get; set; }
    public IEnumerable<UserAdvertReport> UserAdvertReports { get; set; }
}
