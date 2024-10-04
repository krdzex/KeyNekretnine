using Google.Analytics.Data.V1Beta;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Analytic.Queries.GetTopSectionAnalyticForAdvert;
internal sealed class GetTopSectionAnalyticForAdvertHandler : IQueryHandler<GetTopSectionAnalyticForAdvertQuery, TopSectionAnalyticsResponse>
{
    private readonly BetaAnalyticsDataClient _analyticsDataClient;

    public GetTopSectionAnalyticForAdvertHandler(BetaAnalyticsDataClient analyticsDataClient)
    {
        _analyticsDataClient = analyticsDataClient;
    }

    public async Task<Result<TopSectionAnalyticsResponse>> Handle(GetTopSectionAnalyticForAdvertQuery request, CancellationToken cancellationToken)
    {
        var ga4request = new BatchRunReportsRequest
        {
            Property = $"properties/379624338",
            Requests =
        {
            new RunReportRequest
            {
                Dimensions = { new Dimension { Name = "pagePath" } },
                Metrics = { new Metric { Name = "screenPageViews" } },
                DateRanges = { new DateRange { StartDate = "2024-01-01", EndDate = "2024-12-31" } },
                DimensionFilter = new FilterExpression
                {
                    Filter = new Filter
                    {
                        FieldName = "pagePath",
                        StringFilter = new Filter.Types.StringFilter
                        {
                            MatchType = Filter.Types.StringFilter.Types.MatchType.Exact,
                            Value = $"/oglasi/{request.ReferenceId}"
                        }
                    }
                }
            },

            new RunReportRequest
            {
                Dimensions = { new Dimension { Name = "pagePath" } },
                Metrics = { new Metric { Name = "averageSessionDuration" } },
                DateRanges = { new DateRange { StartDate = "2024-01-01", EndDate = "2024-12-31" } },
                DimensionFilter = new FilterExpression
                {
                    Filter = new Filter
                    {
                        FieldName = "pagePath",
                        StringFilter = new Filter.Types.StringFilter
                        {
                            MatchType = Filter.Types.StringFilter.Types.MatchType.Exact,
                            Value = $"/oglasi/{request.ReferenceId}"
                        }
                    }
                }
            },

            new RunReportRequest
            {
                Dimensions = { new Dimension { Name = "pagePath" } },
                Metrics = { new Metric { Name = "bounceRate" } },
                DateRanges = { new DateRange { StartDate = "2024-01-01", EndDate = "2024-12-31" } },
                DimensionFilter = new FilterExpression
                {
                    Filter = new Filter
                    {
                        FieldName = "pagePath",
                        StringFilter = new Filter.Types.StringFilter
                        {
                            MatchType = Filter.Types.StringFilter.Types.MatchType.Exact,
                            Value = $"/oglasi/{request.ReferenceId}"
                        }
                    }
                }
            },
            new RunReportRequest
            {
                Dimensions = { new Dimension { Name = "pagePath" } },
                Metrics = { new Metric { Name = "keyEvents" } },
                DateRanges = { new DateRange { StartDate = "2024-01-01", EndDate = "2024-12-31" } },
                DimensionFilter = new FilterExpression
                {
                    Filter = new Filter
                    {
                        FieldName = "pagePath",
                        StringFilter = new Filter.Types.StringFilter
                        {
                            MatchType = Filter.Types.StringFilter.Types.MatchType.Exact,
                            Value = $"/oglasi/{request.ReferenceId}"
                        }
                    }
                }
            },
            new RunReportRequest
            {
                Dimensions = { new Dimension { Name = "pagePath" } },
                Metrics = { new Metric { Name = "sessionKeyEventRate" } },
                DateRanges = { new DateRange { StartDate = "2024-01-01", EndDate = "2024-12-31" } },
                DimensionFilter = new FilterExpression
                {
                    Filter = new Filter
                    {
                        FieldName = "pagePath",
                        StringFilter = new Filter.Types.StringFilter
                        {
                            MatchType = Filter.Types.StringFilter.Types.MatchType.Exact,
                            Value = $"/oglasi/{request.ReferenceId}"
                        }
                    }
                }
            }
        }
        };

        var ga4response = await _analyticsDataClient.BatchRunReportsAsync(ga4request, cancellationToken);

        var response = new TopSectionAnalyticsResponse
        {
            TotalViews = ga4response.Reports[0].Rows.Any() ? int.Parse(ga4response.Reports[0].Rows[0].MetricValues[0].Value) : 0,
            AvgTimeOnPage = ga4response.Reports[1].Rows.Any()
                ? FormatTime(double.Parse(ga4response.Reports[1].Rows[0].MetricValues[0].Value))
                : "0m 0s",
            BounceRate = ga4response.Reports[2].Rows.Any() ? Double.Round(double.Parse(ga4response.Reports[2].Rows[0].MetricValues[0].Value), 2) * 100 + "%" : "0%",
            LeadsCount = ga4response.Reports[3].Rows.Any() ? int.Parse(ga4response.Reports[3].Rows[0].MetricValues[0].Value) : 0,
            LeadPressProcentage = ga4response.Reports[4].Rows.Any() ? Double.Round(double.Parse(ga4response.Reports[4].Rows[0].MetricValues[0].Value), 2) * 100 + "%" : "0%",
        };

        return Result.Success(response);
    }
    private string FormatTime(double seconds)
    {
        var timeSpan = TimeSpan.FromSeconds(seconds);
        return $"{timeSpan.Minutes}m {timeSpan.Seconds}s";
    }
}