using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.RejectReasons.Queries.Get;
public sealed class RejectReasonResponse
{
    public int Id { get; set; }
    private string ReasonSr { get; set; }
    private string ReasonEn { get; set; }
    public DifferentLanguageResponse Reason { get { return new DifferentLanguageResponse { Sr = ReasonSr, En = ReasonEn }; } }
}
