using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Analytic.Queries.GetTopSectionAnalyticForAdvert;
public sealed record GetTopSectionAnalyticForAdvertQuery(string ReferenceId) : IQuery<TopSectionAnalyticsResponse>;
