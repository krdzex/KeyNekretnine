using MediatR;
using Shared.DataTransferObjects.AdvertStatus;

namespace Application.Queries.AdvertStatusesQueries;
public sealed record GetAdvertStatusesQuery() : IRequest<IEnumerable<AdvertStatusDto>>;
