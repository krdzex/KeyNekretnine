using Application.Abstraction.Messaging;
using Contracts;
using Shared.DataTransferObjects.AdvertType;
using Shared.Error;

namespace Application.Core.AdvertTypes.Queries.GetAdvertTypes;
internal sealed class GetAdvertTypesHandler : IQueryHandler<GetAdvertTypesQuery, List<AdvertTypeDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertTypesHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<AdvertTypeDto>>> Handle(GetAdvertTypesQuery request, CancellationToken cancellationToken)
    {
        var advertTypes = await _repository.AdvertType.GetAdvertTypes(cancellationToken);

        return advertTypes.ToList();
    }
}