using Application.Queries.NeighborhoodsQueries;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.DataTransferObjects.Neighborhood;

namespace Application.Handlers.NeighborhoodsHandlers;
internal sealed class GetNeighborhoodsHandler : IRequestHandler<GetNeighborhoodsQuery, IEnumerable<NeighborhoodDto>>
{
    private readonly IRepositoryManager _repository;

    public GetNeighborhoodsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<NeighborhoodDto>> Handle(GetNeighborhoodsQuery request, CancellationToken cancellationToken)
    {
        var neighborhoods = await _repository.Neighborhood.GetNeighborhoods(request.Id, cancellationToken);

        if (neighborhoods is null)
        {
            throw new NeighborhoodsNotFoundException(request.Id);
        }

        return neighborhoods;
    }
}
