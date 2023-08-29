using Contracts;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.RejectReason;
using Shared.Error;

namespace KeyNekretnine.Application.Core.RejectReasons.Queries.GetRejectReasons;
internal sealed class GetRejectReasonsHandler : IQueryHandler<GetRejectReasonsQuery, List<RejectReasonDto>>
{
    private readonly IRepositoryManager _repository;

    public GetRejectReasonsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<RejectReasonDto>>> Handle(GetRejectReasonsQuery request, CancellationToken cancellationToken)
    {
        var rejectReasons = await _repository.RejectReason.GetRejectReasons(cancellationToken);

        return rejectReasons.ToList();
    }
}