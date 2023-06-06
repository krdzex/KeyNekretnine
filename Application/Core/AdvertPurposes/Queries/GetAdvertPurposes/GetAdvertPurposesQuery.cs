using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.AdvertPurpose;

namespace Application.Core.AdvertPurposes.Queries.GetAdvertPurposes;
public sealed record GetAdvertPurposesQuery() : IQuery<List<AdvertPurposeDto>>;

