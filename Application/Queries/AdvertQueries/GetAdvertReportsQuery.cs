using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Queries.AdvertQueries;
public sealed record GetAdvertReportsQuery() : IRequest<IEnumerable<AdvertReportsDto>>;
