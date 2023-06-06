using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Neighborhood;

namespace Application.Core.Neighborhoods.Queries;

public sealed record GetNeighborhoodsByCityIdQuery(int CityId) : IQuery<List<NeighborhoodDto>>;