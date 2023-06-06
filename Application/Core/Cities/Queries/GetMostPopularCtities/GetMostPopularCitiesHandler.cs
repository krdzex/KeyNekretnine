using Application.Abstraction.Messaging;
using Contracts;
using Shared.DataTransferObjects.City;
using Shared.Error;

namespace Application.Core.Cities.Queries.GetMostPopularCtities;
internal sealed class GetMostPopularCitiesHandler : IQueryHandler<GetMostPopularCitiesQuery, List<PopularCitiesDto>>
{
    private readonly IRepositoryManager _repository;

    public GetMostPopularCitiesHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<PopularCitiesDto>>> Handle(GetMostPopularCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _repository.City.GetMostPopularCities(cancellationToken);

        return cities.ToList();
    }
}
