using Application.Queries.CityQueries;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.City;

namespace Application.Handlers.CityHandlers;
internal sealed class GetCitiesHandler : IRequestHandler<GetCitiesQuery, IEnumerable<CityDto>>
{
    private readonly IRepositoryManager _repository;

    public GetCitiesHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _repository.City.GetCities(cancellationToken);

        return cities;
    }
}

