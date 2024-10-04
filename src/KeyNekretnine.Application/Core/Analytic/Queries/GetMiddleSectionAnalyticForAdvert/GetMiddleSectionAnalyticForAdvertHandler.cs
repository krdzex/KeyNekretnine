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
                Dimensions = { new Dimension { Name = "month" } },
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
                },
            },
            new RunReportRequest
            {
                Dimensions = { new Dimension { Name = "month" } },
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
        }
        };

        var ga4response = await _analyticsDataClient.BatchRunReportsAsync(ga4request, cancellationToken);

        var viewsOverTime = InitializeMonthDictionaryWithZeroValues();
        var keyEventsOverTime = InitializeMonthDictionaryDoubleWithZeroValues();

        foreach (var row in ga4response.Reports[0].Rows)
        {
            var month = int.Parse(row.DimensionValues[0].Value); // "01", "02", etc.
            var views = int.Parse(row.MetricValues[0].Value);
            viewsOverTime[month] = views; // Replace the default 0 if data exists
        }

        // Parse the response for key events
        foreach (var row in ga4response.Reports[1].Rows)
        {
            var month = int.Parse(row.DimensionValues[0].Value);
            var keyEvents = double.Parse(row.MetricValues[0].Value);
            keyEventsOverTime[month] = keyEvents; // Replace the default 0 if data exists
        }

        var monthNames = GetMonthNames();

        var orderedViewsOverTime = viewsOverTime
            .OrderByDescending(x => x.Key)
            .ToDictionary(x => monthNames[x.Key], x => x.Value); // Use month names

        var orderedKeyEventsOverTime = keyEventsOverTime
            .OrderByDescending(x => x.Key)
            .ToDictionary(x => monthNames[x.Key], x => x.Value); // Use month names

        // Create the response object and return the ordered dictionaries
        var response = new MiddleSectionAnalyticForAdvertResponse
        {
            ViewsOverTime = orderedViewsOverTime,
            KeyEventsOverTime = orderedKeyEventsOverTime
        };

        return Result.Success(response);
    }

    private Dictionary<int, int> InitializeMonthDictionaryWithZeroValues()
    {
        return Enumerable.Range(1, 12).ToDictionary(month => month, month => 0);
    }

    private Dictionary<int, double> InitializeMonthDictionaryDoubleWithZeroValues()
    {
        return Enumerable.Range(1, 12).ToDictionary(month => month, month => 0.0);
    }

    // Helper method to map month numbers to their names
    private Dictionary<int, string> GetMonthNames()
    {
        return new Dictionary<int, string>
        {
            { 1, "January" },
            { 2, "February" },
            { 3, "March" },
            { 4, "April" },
            { 5, "May" },
            { 6, "June" },
            { 7, "July" },
            { 8, "August" },
            { 9, "September" },
            { 10, "October" },
            { 11, "November" },
            { 12, "December" }
        };
    }
}