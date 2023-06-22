using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.City;

namespace Application.Core.Cities.Queries.GetMostPopularCtities;
public sealed record GetMostPopularCitiesQuery() : IQuery<List<PopularCitiesDto>>;