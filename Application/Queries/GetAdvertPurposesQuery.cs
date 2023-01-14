using MediatR;
using Shared.DataTransferObjects.AdvertPurpose;

namespace Application.Queries;
public sealed record GetAdvertPurposesQuery() : IRequest<IEnumerable<ShowAdvertPurposeDto>>;

