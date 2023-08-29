using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Neighborhood;

namespace KeyNekretnine.Application.Core.Neighborhoods.Queries;

public sealed record GetNeighborhoodsByCityIdQuery(int CityId) : IQuery<List<NeighborhoodDto>>;