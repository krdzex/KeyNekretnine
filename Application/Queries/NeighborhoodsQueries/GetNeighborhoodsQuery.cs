using MediatR;
using Shared.DataTransferObjects.Neighborhood;

namespace Application.Queries.NeighborhoodsQueries;
public sealed record GetNeighborhoodsQuery(int Id) : IRequest<IEnumerable<ShowNeighborhoodDto>>;

