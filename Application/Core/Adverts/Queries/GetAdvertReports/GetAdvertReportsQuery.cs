using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertReports;
public sealed record GetAdvertReportsQuery(ReportParameters ReportParameters) : IQuery<Pagination<AdvertReportsDto>>;