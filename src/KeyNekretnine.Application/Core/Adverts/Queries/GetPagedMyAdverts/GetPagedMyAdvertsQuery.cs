using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared.Pagination;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetPagedMyAdverts;
public sealed record GetPagedMyAdvertsQuery(
    string OrderBy,
    int PageNumber,
    int PageSize,
    int? Type,
    int? Purpose,
    int? Status) : IQuery<Pagination<PagedMyAdvertResponse>>;