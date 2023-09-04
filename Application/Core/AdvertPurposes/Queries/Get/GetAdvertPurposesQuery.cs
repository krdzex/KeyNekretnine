using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.AdvertPurposes.Queries.Get;
public sealed record GetAdvertPurposesQuery() : IQuery<IReadOnlyList<AdvertPurposeResponse>>;