namespace KeyNekretnine.Application.Core.Analytic.Queries.GetMiddleSectionAnalyticForAdvert;
public class MiddleSectionAnalyticForAdvertResponse
{
    public List<MiddleSectionAnalyticForAdvertItem> ViewsOverTime { get; set; } = new();
    public List<MiddleSectionAnalyticForAdvertItem> KeyEventsOverTime { get; set; } = new();
}

public class MiddleSectionAnalyticForAdvertItem
{
    public string Date { get; set; }
    public int Views { get; set; }
}