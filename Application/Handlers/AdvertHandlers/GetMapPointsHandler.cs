using Application.Queries.AdvertQuery;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetMapPointsHandler : IRequestHandler<GetMapPointsQuery, IEnumerable<ShowAdvertLocationOnMapDto>>
{
    private readonly IRepositoryManager _repository;

    public GetMapPointsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<ShowAdvertLocationOnMapDto>> Handle(GetMapPointsQuery request, CancellationToken cancellationToken)
    {
        var mapLocations = await _repository.Advert.GetMapPoints(cancellationToken);

        return mapLocations;
    }
}
