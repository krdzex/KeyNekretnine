﻿namespace KeyNekretnine.Application.Core.Analytic.Queries.GetTopSectionAnalyticForAdvert;
public class TopSectionAnalyticsResponse
{
    public int TotalViews { get; set; }
    public string AvgTimeOnPage { get; set; }
    public string BounceRate { get; set; }
    public int LeadsCount { get; set; }
    public string LeadPressProcentage { get; set; }
}