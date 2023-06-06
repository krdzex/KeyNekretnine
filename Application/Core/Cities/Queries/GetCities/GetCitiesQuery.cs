using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.City;

namespace Application.Cities.Queries.GetCities;
public sealed record GetCitiesQuery() : IQuery<List<CityDto>>;
