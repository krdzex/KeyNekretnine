using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAllAdvertCoordinates;
public sealed record GetAllAdvertCoordinatesQuery() : IQuery<IReadOnlyList<AdvertCoordinateResponse>>;