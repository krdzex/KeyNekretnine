using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetIsAdvertFavorite;
public sealed record GetIsAdvertFavoriteQuery(int AdvertId, string Email) : IQuery<bool>;