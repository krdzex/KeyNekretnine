using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared.Pagination;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertUpdates;
public sealed record GetAdvertUpdatesQuery(
    string OrderBy,
    int PageNumber,
    int PageSize,
    int? UpdateType,
    string ReferenceId) : IQuery<Pagination<AdvertForUpdateResponse>>;