using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.AdvertStatuses.Queries.Get;
public sealed record GetAdvertStatusesQuery() : IQuery<IReadOnlyList<AdvertStatusResponse>>;