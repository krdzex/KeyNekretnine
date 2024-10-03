using Google.Analytics.Data.V1Beta;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Application.Core.Analytic.Queries.GetMiddleSectionAnalyticForAdvert;
internal sealed class GetMiddleSectionAnalyticForAdvertHandler : IQueryHandler<GetMiddleSectionAnalyticForAdvertQuery, MiddleSectionAnalyticForAdvertResponse>
{
    private readonly BetaAnalyticsDataClient _analyticsDataClient;

    public GetMiddleSectionAnalyticForAdvertHandler(BetaAnalyticsDataClient analyticsDataClient)
    {
        _analyticsDataClient = analyticsDataClient;
    }

    public async Task<Result<MiddleSectionAnalyticForAdvertResponse>> Handle(GetMiddleSectionAnalyticForAdvertQuery request, CancellationToken cancellationToken)
    {
        var ga4request = new BatchRunReportsRequest
        {
            Property = "properties/379624338",
            Requests =
        {
            new RunReportRequest
            {
                Dimensions = { new Dimension { Name = "date" } },
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
                Dimensions = { new Dimension { Name = "date" } },
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
                Dimensions = { new Dimension { Name = "date" } },
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
            }
        }
        };

        var ga4response = await _analyticsDataClient.BatchRunReportsAsync(ga4request, cancellationToken);

        var viewsOverTime = new Dictionary<string, int>();
        foreach (var row in ga4response.Reports[0].Rows)
        {
            var date = row.DimensionValues[0].Value;
            var views = int.Parse(row.MetricValues[0].Value);
            viewsOverTime[date] = views;
        }

        var avgTimeOnPageOverTime = new Dictionary<string, double>();
        foreach (var row in ga4response.Reports[1].Rows)
        {
            var date = row.DimensionValues[0].Value;
            var avgTime = double.Parse(row.MetricValues[0].Value);
            avgTimeOnPageOverTime[date] = avgTime;
        }

        var bounceRateOverTime = new Dictionary<string, double>();
        foreach (var row in ga4response.Reports[2].Rows)
        {
            var date = row.DimensionValues[0].Value;
            var bounceRate = double.Parse(row.MetricValues[0].Value);
            bounceRateOverTime[date] = bounceRate;
        }

        var response = new MiddleSectionAnalyticForAdvertResponse
        {
            ViewsOverTime = viewsOverTime,
            AvgTimeOnPageOverTime = avgTimeOnPageOverTime,
            BounceRateOverTime = bounceRateOverTime
        };

        return Result.Success(response);
    }
}