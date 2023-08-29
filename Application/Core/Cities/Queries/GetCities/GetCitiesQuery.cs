using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Cities.Queries.GetCities;
public sealed record GetCitiesQuery() : IQuery<List<CityReponse>>;