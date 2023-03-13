using MediatR;
using Shared.DataTransferObjects.AdvertPurpose;

namespace Application.Queries.AdvertPurposesQueries;
public sealed record GetAdvertPurposesQuery() : IRequest<IEnumerable<AdvertPurposeDto>>;

