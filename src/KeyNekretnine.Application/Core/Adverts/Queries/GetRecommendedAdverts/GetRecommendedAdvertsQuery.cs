using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdverts;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetRecommendedAdverts;
public sealed record GetRecommendedAdvertsQuery(string ReferenceId) : IQuery<IReadOnlyList<PagedAdvertResponse>>;