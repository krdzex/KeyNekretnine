using MediatR;
using Shared.CustomResponses;
using Shared.RequestFeatures;

namespace Application.Queries;
public sealed record GetAdvertsInPaginationQuery(AdvertParameters AdvertParameters) : IRequest<Pagination>;
