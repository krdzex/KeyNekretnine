using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Application.Queries.AdvertQueries;
public sealed record GetFavoriteAdvertsQuery(FavoriteAdvertsParameters RequestParameters, string Email) : IRequest<Pagination<MinimalInformationsAboutAdvertDto>>;
