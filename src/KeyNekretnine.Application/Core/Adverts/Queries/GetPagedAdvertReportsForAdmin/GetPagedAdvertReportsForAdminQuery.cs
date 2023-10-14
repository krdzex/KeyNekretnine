using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared.Pagination;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdvertReportsForAdmin;
public sealed record GetPagedAdvertReportsForAdminQuery(
    string OrderBy,
    int PageNumber,
    int PageSize) : IQuery<Pagination<PagedAdvertReport>>;