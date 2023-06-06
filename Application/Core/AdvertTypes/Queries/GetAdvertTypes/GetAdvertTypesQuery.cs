using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.AdvertType;

namespace Application.Core.AdvertTypes.Queries.GetAdvertTypes;
public sealed record GetAdvertTypesQuery() : IQuery<List<AdvertTypeDto>>;

