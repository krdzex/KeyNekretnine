using Application.Abstraction.Messaging;
using Contracts;
using Shared.DataTransferObjects.City;
using Shared.Error;

namespace Application.Cities.Queries.GetCities;
internal sealed class GetCitiesHandler : IQueryHandler<GetCitiesQuery, List<CityDto>>
{
    private readonly IRepositoryManager _repository;

    public GetCitiesHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<CityDto>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _repository.City.GetCities(cancellationToken);

        return cities.ToList();
    }
}