using MediatR;
using Shared.DataTransferObjects.AdvertType;

namespace Application.Queries.AdvertTypesQueries;
public sealed record GetAdvertTypesQuery() : IRequest<IEnumerable<AdvertTypeDto>>;

