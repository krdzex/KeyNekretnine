using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.AdvertTypes.Queries.Get;
public sealed record GetAdvertTypesQuery() : IQuery<IReadOnlyList<AdvertTypeResponse>>;