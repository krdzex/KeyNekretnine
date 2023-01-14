using Application.Queries;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.DataTransferObjects.Neighborhood;

namespace Application.Handlers;
internal sealed class GetNeighborhoodsHandler : IRequestHandler<GetNeighborhoodsQuery, IEnumerable<ShowNeighborhoodDto>>
{
    private readonly IRepositoryManager _repository;

    public GetNeighborhoodsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<ShowNeighborhoodDto>> Handle(GetNeighborhoodsQuery request, CancellationToken cancellationToken)
    {
        var neighborhoods = await _repository.Neighborhood.GetNeighborhoods(request.Id, cancellationToken);

        if (neighborhoods is null)
        {
            throw new NeighborhoodsNotFoundException(request.Id);
        }
        return neighborhoods;
    }
}
