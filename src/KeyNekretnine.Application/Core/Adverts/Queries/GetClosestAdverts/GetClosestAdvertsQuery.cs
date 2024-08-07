using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetClosestAdverts;
public sealed record GetClosestAdvertsQuery(string ReferenceId) : IQuery<IReadOnlyList<ClosestAdvertsResponse>>;
