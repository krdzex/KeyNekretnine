using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Adverts.Queries.GetPagedMyAdverts;
using KeyNekretnine.Application.Core.Shared.Pagination;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetMyAdverts;
public sealed record GetPagedMyAdvertsQuery(
    string OrderBy,
    int PageNumber,
    int PageSize,
    string UserId,
    int? Type,
    int? Purpose,
    int? Status) : IQuery<Pagination<PagedMyAdvertResponse>>;