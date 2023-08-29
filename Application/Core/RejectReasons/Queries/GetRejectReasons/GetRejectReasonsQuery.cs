using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.RejectReason;

namespace KeyNekretnine.Application.Core.RejectReasons.Queries.GetRejectReasons;
public sealed record GetRejectReasonsQuery() : IQuery<List<RejectReasonDto>>;