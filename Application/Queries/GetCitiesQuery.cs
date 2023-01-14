using MediatR;
using Shared.DataTransferObjects.City;

namespace Application.Queries;
public sealed record GetCitiesQuery() : IRequest<IEnumerable<ShowCityDto>>;
