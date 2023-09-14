using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.RejectReasons.Queries.GetRejectReasons;
public sealed record GetRejectReasonsQuery() : IQuery<IReadOnlyList<RejectReasonResponse>>;