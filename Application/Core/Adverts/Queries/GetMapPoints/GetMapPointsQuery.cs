using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetMapPoints;
public sealed record GetMapPointsQuery() : IQuery<List<ShowAdvertLocationOnMapDto>>;