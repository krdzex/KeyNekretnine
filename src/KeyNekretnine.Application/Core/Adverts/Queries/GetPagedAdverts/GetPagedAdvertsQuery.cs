using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared.Pagination;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdverts;
public sealed record GetPagedAdvertsQuery(
    string OrderBy,
    int PageNumber,
    int PageSize,
    int MinPrice,
    int MaxPrice,
    int MinFloorSpace,
    int MaxFloorSpace,
    List<int> NoOfBedrooms,
    List<int> NoOfBathrooms,
    int? Type,
    int? Purpose,
    List<int> Neighborhoods,
    string CitySlug,
    bool? IsUrgent,
    bool? IsUnderConstruction,
    bool? IsFurnished) : IQuery<Pagination<PagedAdvertResponse>>;