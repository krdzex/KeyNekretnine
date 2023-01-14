using MediatR;
using Shared.DataTransferObjects.Neighborhood;

namespace Application.Queries;
public sealed record GetNeighborhoodsQuery(int Id) : IRequest<IEnumerable<ShowNeighborhoodDto>>;

