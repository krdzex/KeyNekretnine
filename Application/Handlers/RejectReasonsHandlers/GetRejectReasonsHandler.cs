using Application.Queries.RejectReasonsQueries;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.RejectReason;

namespace Application.Handlers.RejectReasonsHandlers;
internal sealed class GetRejectReasonsHandler : IRequestHandler<GetRejectReasonsQuery, IEnumerable<RejectReasonDto>>
{
    private readonly IRepositoryManager _repository;

    public GetRejectReasonsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<RejectReasonDto>> Handle(GetRejectReasonsQuery request, CancellationToken cancellationToken)
    {
        var rejectReasons = await _repository.RejectReason.GetRejectReasons(cancellationToken);

        return rejectReasons;
    }
}
