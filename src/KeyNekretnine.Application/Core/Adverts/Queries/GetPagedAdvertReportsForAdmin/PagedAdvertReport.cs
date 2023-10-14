namespace KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdvertReportsForAdmin;
public class PagedAdvertReport
{
    public string ReferenceId { get; set; }
    public int RepeatingAdvert { get; set; }
    public int BadImages { get; set; }
    public int BadInformations { get; set; }
    public int AllReports { get; set; }
}
