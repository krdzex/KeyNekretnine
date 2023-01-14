using Application.Queries;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.City;

namespace Application.Handlers;

internal sealed class GetCitiesHandler : IRequestHandler<GetCitiesQuery, IEnumerable<ShowCityDto>>
{
    private readonly IRepositoryManager _repository;

    public GetCitiesHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<ShowCityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _repository.City.GetCities(cancellationToken);

        return cities;
    }
}

