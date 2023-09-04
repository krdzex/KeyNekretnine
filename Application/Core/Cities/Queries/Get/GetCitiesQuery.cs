using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Cities.Queries.Get;
public sealed record GetCitiesQuery() : IQuery<IReadOnlyList<CityReponse>>;