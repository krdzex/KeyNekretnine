using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace Application.Core.Adverts.Queries.GetmapPoints;
public sealed record GetMapPointsQuery() : IQuery<List<ShowAdvertLocationOnMapDto>>;

