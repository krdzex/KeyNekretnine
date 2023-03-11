using MediatR;

namespace Application.Queries.AdvertQueries;
public sealed record GetIsFavoriteAdvertQuery(int AdvertId, string Email) : IRequest<bool>;
