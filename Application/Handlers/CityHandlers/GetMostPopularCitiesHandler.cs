using Application.Queries.CityQueries;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.City;

namespace Application.Handlers.CityHandlers;
internal sealed class GetMostPopularCitiesHandler : IRequestHandler<GetMostPopularCitiesQuery, IEnumerable<PopularCitiesDto>>
{
    private readonly IRepositoryManager _repository;

    public GetMostPopularCitiesHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<PopularCitiesDto>> Handle(GetMostPopularCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _repository.City.GetMostPopularCities(cancellationToken);

        return cities;
    }
}
