using Contracts;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetMapPoints;
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