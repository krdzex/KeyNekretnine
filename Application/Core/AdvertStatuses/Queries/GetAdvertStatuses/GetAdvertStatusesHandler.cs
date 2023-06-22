using Application.Abstraction.Messaging;
using Contracts;
using Shared.DataTransferObjects.AdvertStatus;
using Shared.Error;

namespace Application.Core.AdvertStatuses.Queries.GetAdvertStatuses;
internal sealed class GetAdvertStatusesHandler : IQueryHandler<GetAdvertStatusesQuery, List<AdvertStatusDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertStatusesHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<AdvertStatusDto>>> Handle(GetAdvertStatusesQuery request, CancellationToken cancellationToken)
    {
        var advertStatuses = await _repository.AdvertStatus.GetAdvertsStatuses(cancellationToken);

        return advertStatuses.ToList();
    }
}