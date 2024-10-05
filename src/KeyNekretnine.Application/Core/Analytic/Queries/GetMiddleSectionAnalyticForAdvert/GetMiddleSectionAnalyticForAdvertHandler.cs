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
        var endDate = DateTime.Now.ToString("yyyy-MM-dd");
        var startDate = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");

        var ga4request = new BatchRunReportsRequest
        {
            Property = "properties/379624338",
            Requests =
        {
            new RunReportRequest
            {
                Dimensions = { new Dimension { Name = "date" } },
                Metrics = { new Metric { Name = "screenPageViews" } },
                DateRanges = { new DateRange { StartDate = startDate, EndDate = endDate } },
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
                },
            },
            new RunReportRequest
            {
                Dimensions = { new Dimension { Name = "date" } },
                Metrics = { new Metric { Name = "keyEvents" } },
                DateRanges = { new DateRange { StartDate = startDate, EndDate = endDate } },
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
        }
        };

        var ga4response = await _analyticsDataClient.BatchRunReportsAsync(ga4request, cancellationToken);

        var response = new MiddleSectionAnalyticForAdvertResponse();

        foreach (var row in ga4response.Reports[0].Rows)
        {
            var date = row.DimensionValues[0].Value;
            var formattedDate = DateTime.ParseExact(date, "yyyyMMdd", null).ToString("yyyy-MM-dd");

            var views = int.Parse(row.MetricValues[0].Value);

            response.ViewsOverTime.Add(new MiddleSectionAnalyticForAdvertItem
            {
                Date = formattedDate,
                Views = views
            });
        }

        foreach (var row in ga4response.Reports[1].Rows)
        {
            var date = row.DimensionValues[0].Value;
            var formattedDate = DateTime.ParseExact(date, "yyyyMMdd", null).ToString("yyyy-MM-dd");

            var views = int.Parse(row.MetricValues[0].Value);

            response.KeyEventsOverTime.Add(new MiddleSectionAnalyticForAdvertItem
            {
                Date = formattedDate,
                Views = views
            });
        }

        return Result.Success(response);
    }
}