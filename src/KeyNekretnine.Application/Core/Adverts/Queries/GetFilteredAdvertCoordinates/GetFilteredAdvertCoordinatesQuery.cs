using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetFilteredAdvertCoordinates;
public sealed record GetFilteredAdvertCoordinatesQuery(
    int MinPrice,
    int MaxPrice,
    int MinFloorSpace,
    int MaxFloorSpace,
    List<int> NoOfBedrooms,
    List<int> NoOfBathrooms,
    List<int> Types,
    List<int> Purposes,
    List<int> Neighborhoods,
    int? CityId,
    bool? IsUrgent,
    bool? IsUnderConstruction,
    bool? IsFurnished) : IQuery<IReadOnlyList<AdvertCoordinateResponse>>;