using MediatR;
using Shared.DataTransferObjects.RejectReason;

namespace Application.Queries.RejectReasonsQueries;
public sealed record GetRejectReasonsQuery() : IRequest<IEnumerable<RejectReasonDto>>;

