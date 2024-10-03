namespace KeyNekretnine.Application.Core.Analytic.Queries.GetMiddleSectionAnalyticForAdvert;
public class MiddleSectionAnalyticForAdvertResponse
{
    public Dictionary<string, int> ViewsOverTime { get; set; } // Date -> Views
    public Dictionary<string, double> AvgTimeOnPageOverTime { get; set; } // Date -> Average Time in Seconds
    public Dictionary<string, double> BounceRateOverTime { get; set; } // Date -> Bounce Rate (percentage)
}