using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Cities.Queries.GetMostPopularCities;
public sealed record GetMostPopularCitiesQuery() : IQuery<IReadOnlyList<PopularCityReponse>>;