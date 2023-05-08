using Application.Queries.AdvertStatusesQueries;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.AdvertStatus;

namespace Application.Handlers.AdvertStatusHandler;
internal sealed class GetAdvertStatusesHandler : IRequestHandler<GetAdvertStatusesQuery, IEnumerable<AdvertStatusDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertStatusesHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<AdvertStatusDto>> Handle(GetAdvertStatusesQuery request, CancellationToken cancellationToken)
    {
        var advertStatuses = await _repository.AdvertStatus.GetAdvertsStatuses(cancellationToken);

        return advertStatuses;
    }
}

