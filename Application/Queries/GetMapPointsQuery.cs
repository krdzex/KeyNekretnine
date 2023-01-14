using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Queries;
public sealed record GetMapPointsQuery() : IRequest<IEnumerable<ShowAdvertLocationOnMapDto>>;

