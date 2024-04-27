using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared.Pagination;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetFavoriteAdverts;
public sealed record GetFavoriteAdvertsQuery(
    string OrderBy,
    int PageNumber,
    int PageSize) : IQuery<Pagination<FavoriteAdvertResponse>>;