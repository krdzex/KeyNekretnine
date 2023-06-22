using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.RejectReason;

namespace Application.Core.RejectReasons.Queries.GetRejectReasons;
public sealed record GetRejectReasonsQuery() : IQuery<List<RejectReasonDto>>;