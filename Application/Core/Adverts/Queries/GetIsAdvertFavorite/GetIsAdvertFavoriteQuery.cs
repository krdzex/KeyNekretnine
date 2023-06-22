using Application.Abstraction.Messaging;

namespace Application.Core.Adverts.Queries.GetIsFavorite;
public sealed record GetIsAdvertFavoriteQuery(int AdvertId, string Email) : IQuery<bool>;