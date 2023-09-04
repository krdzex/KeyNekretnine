using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared.Pagination;

namespace KeyNekretnine.Application.Core.Agencies.Queries.Get;
public sealed record GetAgenciesQuery(
    string OrderBy,
    int PageNumber,
    int PageSize,
    string Name) : IQuery<Pagination<PaginationAgencyResponse>>;