using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAllAdvertCoordinates;
public sealed record GetAllAdvertCoordinatesQuery() : IQuery<IReadOnlyList<AdvertCoordinateResponse>>;