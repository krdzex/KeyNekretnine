using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using Shared.DataTransferObjects.Neighborhood;
using Shared.Error;

namespace Application.Core.Neighborhoods.Queries;
internal sealed class GetNeighborhoodsByCityIdHandler : IQueryHandler<GetNeighborhoodsByCityIdQuery, List<NeighborhoodDto>>
{
    private readonly IRepositoryManager _repository;

    public GetNeighborhoodsByCityIdHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<NeighborhoodDto>>> Handle(GetNeighborhoodsByCityIdQuery request, CancellationToken cancellationToken)
    {
        var neighborhoods = await _repository.Neighborhood.GetNeighborhoods(request.CityId, cancellationToken);

        if (!neighborhoods.Any())
        {
            return Result.Failure<List<NeighborhoodDto>>(DomainErrors.Neighborhood.NeighborhoodNotFound);
        }

        return neighborhoods.ToList();
    }
}