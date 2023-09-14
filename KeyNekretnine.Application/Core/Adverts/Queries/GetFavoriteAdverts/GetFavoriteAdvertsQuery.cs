using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetFavoriteAdverts;
public sealed record GetFavoriteAdvertsQuery(FavoriteAdvertsParameters RequestParameters, string Email) : IQuery<Pagination<MinimalInformationsAboutAdvertDto>>;