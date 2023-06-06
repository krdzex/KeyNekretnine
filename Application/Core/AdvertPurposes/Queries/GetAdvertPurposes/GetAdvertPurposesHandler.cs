using Application.Abstraction.Messaging;
using Contracts;
using Shared.DataTransferObjects.AdvertPurpose;
using Shared.Error;

namespace Application.Core.AdvertPurposes.Queries.GetAdvertPurposes;

internal sealed class GetAdvertPurposesHandler : IQueryHandler<GetAdvertPurposesQuery, List<AdvertPurposeDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertPurposesHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<AdvertPurposeDto>>> Handle(GetAdvertPurposesQuery request, CancellationToken cancellationToken)
    {
        var advertPurposes = await _repository.AdvertPurpose.GetAdvertPurposes(cancellationToken);

        return advertPurposes.ToList();
    }
}

