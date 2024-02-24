using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Neighborhoods.Queries.GetNeighborhoodsByCityId;

public sealed record GetNeighborhoodsByCityIdQuery(string CitySlug) : IQuery<IReadOnlyList<NeighborhoodResponse>>;