using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.AdvertPurposes.Queries.GetAdvertPurposes;
public sealed record GetAdvertPurposesQuery() : IQuery<IReadOnlyList<AdvertPurposeResponse>>;