using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Neighborhoods.Queries.GetNeighborhoodsByCityId;

public sealed record GetNeighborhoodsByCityIdQuery(int CityId) : IQuery<IReadOnlyList<NeighborhoodResponse>>;