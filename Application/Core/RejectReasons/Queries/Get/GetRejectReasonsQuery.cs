using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.RejectReasons.Queries.Get;
public sealed record GetRejectReasonsQuery() : IQuery<IReadOnlyList<RejectReasonResponse>>;