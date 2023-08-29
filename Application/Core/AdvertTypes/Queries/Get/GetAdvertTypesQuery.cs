using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.AdvertTypes.Queries.GetAdvertTypes;
public sealed record GetAdvertTypesQuery() : IQuery<IReadOnlyList<AdvertTypeResponse>>;