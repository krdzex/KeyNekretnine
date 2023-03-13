using MediatR;
using Shared.DataTransferObjects.City;

namespace Application.Queries.CityQueries;
public sealed record GetCitiesQuery() : IRequest<IEnumerable<CityDto>>;
