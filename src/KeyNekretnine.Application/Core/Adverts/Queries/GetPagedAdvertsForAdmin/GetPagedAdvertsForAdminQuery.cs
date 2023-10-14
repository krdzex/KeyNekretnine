using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared.Pagination;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdvertsForAdmin;
public sealed record GetPagedAdvertsForAdminQuery(
    string OrderBy,
    int PageNumber,
    int PageSize,
    string? ReferenceId,
    int? Type,
    int? Purpose,
    int? Status) : IQuery<Pagination<PagedAdvertForAdminResponse>>;