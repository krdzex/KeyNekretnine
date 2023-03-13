namespace Entities.Models;
public class UserAdvertReport
{
    public string UserId { get; set; }
    public User User { get; set; }
    public int AdvertId { get; set; }
    public Advert Advert { get; set; }
    public int RejectionReasonId { get; set; }
    public RejectReason RejectionReason { get; set; }
}
