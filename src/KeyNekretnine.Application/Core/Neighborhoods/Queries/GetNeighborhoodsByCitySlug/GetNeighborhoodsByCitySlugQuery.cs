using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Neighborhoods.Queries.GetNeighborhoodsByCitySlug;

public sealed record GetNeighborhoodsByCitySlugQuery(string CitySlug) : IQuery<IReadOnlyList<NeighborhoodResponse>>;