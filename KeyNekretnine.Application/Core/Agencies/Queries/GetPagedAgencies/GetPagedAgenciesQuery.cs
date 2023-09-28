using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared.Pagination;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencies;
public sealed record GetPagedAgenciesQuery(
    string OrderBy,
    int PageNumber,
    int PageSize,
    string Name) : IQuery<Pagination<PagedAgencyResponse>>;