using KeyNekretnine.Domain.Models;

namespace KeyNekretnine.Domain.RejectReasons;
public class RejectReason : EntityBase
{
    public string ReasonSr { get; set; }
    public string ReasonEn { get; set; }
}