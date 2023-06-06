using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.AdvertStatus;

namespace Application.Core.AdvertStatuses.Queries.GetAdvertStatuses;
public sealed record GetAdvertStatusesQuery() : IQuery<List<AdvertStatusDto>>;
