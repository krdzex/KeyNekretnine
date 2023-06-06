using Application.Abstraction.Messaging;
using Contracts;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace Application.Core.Adverts.Queries.GetmapPoints;
internal sealed class GetMapPointsHandler : IQueryHandler<GetMapPointsQuery, List<ShowAdvertLocationOnMapDto>>
{
    private readonly IRepositoryManager _repository;

    public GetMapPointsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<ShowAdvertLocationOnMapDto>>> Handle(GetMapPointsQuery request, CancellationToken cancellationToken)
    {
        var mapLocations = await _repository.Advert.GetMapPoints(cancellationToken);

        return mapLocations.ToList();
    }
}
