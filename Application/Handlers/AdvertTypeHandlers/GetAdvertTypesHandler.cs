using Application.Queries.AdvertTypesQueries;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.AdvertType;

namespace Application.Handlers.AdvertTypeHandlers;
internal sealed class GetAdvertTypesHandler : IRequestHandler<GetAdvertTypesQuery, IEnumerable<ShowAdvertTypeDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertTypesHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<ShowAdvertTypeDto>> Handle(GetAdvertTypesQuery request, CancellationToken cancellationToken)
    {
        var advertTypes = await _repository.AdvertType.GetAdvertTypes(cancellationToken);

        return advertTypes;
    }
}