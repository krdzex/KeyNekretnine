namespace Shared.DataTransferObjects.RejectReason;
public class RejectReasonDto
{
    public int Id { get; set; }
    private string Reason_Sr { get; set; }
    private string Reason_En { get; set; }
    public DifferentLanguagesDto Reason { get { return new DifferentLanguagesDto { Sr = Reason_Sr, En = Reason_En }; } }
}

