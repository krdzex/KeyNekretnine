using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Cities.Queries.GetMostPopular;
public sealed record GetMostPopularCitiesQuery() : IQuery<IReadOnlyList<PopularCityReponse>>;