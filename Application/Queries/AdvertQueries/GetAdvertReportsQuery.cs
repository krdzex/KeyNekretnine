using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Application.Queries.AdvertQueries;
public sealed record GetAdvertReportsQuery(ReportParameters ReportParameters) : IRequest<Pagination<AdvertReportsDto>>;
