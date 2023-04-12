using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Application.Queries.AdvertQueries;
public sealed record GetMyAdvertsQuery(MyAdvertsParameters MyAdvertParameters, string Email) : IRequest<Pagination<MyAdvertsDto>>;