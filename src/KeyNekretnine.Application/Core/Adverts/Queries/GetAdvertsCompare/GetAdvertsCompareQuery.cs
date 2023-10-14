using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertsCompare;
public sealed record GetAdvertsCompareQuery(string FirstReferenceId, string SecondReferenceId) : IQuery<IReadOnlyList<CompareAdvertResponse>>;