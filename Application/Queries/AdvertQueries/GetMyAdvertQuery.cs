using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Queries.AdvertQueries;
public sealed record GetMyAdvertQuery(int AdvertId, string Email) : IRequest<MyAdvertDto>;
