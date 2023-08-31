using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Neighborhoods.Queries;

public sealed record GetByCityIdQuery(int CityId) : IQuery<IReadOnlyList<NeighborhoodResponse>>;