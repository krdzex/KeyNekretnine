using MediatR;
using Shared.DataTransferObjects.City;

namespace Application.Queries.CityQueries;
public sealed record GetMostPopularCitiesQuery() : IRequest<IEnumerable<PopularCitiesDto>>;

