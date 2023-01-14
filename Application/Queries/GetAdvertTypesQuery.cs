using MediatR;
using Shared.DataTransferObjects.AdvertType;

namespace Application.Queries;
public sealed record GetAdvertTypesQuery() : IRequest<IEnumerable<ShowAdvertTypeDto>>;

