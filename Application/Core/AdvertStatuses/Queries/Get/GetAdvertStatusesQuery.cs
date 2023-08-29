using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.AdvertStatuses.Queries.GetAdvertStatuses;
public sealed record GetAdvertStatusesQuery() : IQuery<IReadOnlyList<AdvertStatusResponse>>;