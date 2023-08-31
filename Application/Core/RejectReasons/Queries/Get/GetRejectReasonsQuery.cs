using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.RejectReasons.Queries.Get;

namespace KeyNekretnine.Application.Core.RejectReasons.Queries.GetRejectReasons;
public sealed record GetRejectReasonsQuery() : IQuery<IReadOnlyList<RejectReasonResponse>>;