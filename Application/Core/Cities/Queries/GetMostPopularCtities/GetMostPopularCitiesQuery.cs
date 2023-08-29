using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Cities.Queries.GetMostPopularCtities;
public sealed record GetMostPopularCitiesQuery() : IQuery<IReadOnlyList<PopularCityReponse>>;