using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Queries.AdvertQuery;
public sealed record GetMapPointsQuery() : IRequest<IEnumerable<ShowAdvertLocationOnMapDto>>;

