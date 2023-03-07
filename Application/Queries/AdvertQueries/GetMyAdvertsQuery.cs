using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;

namespace Application.Queries.AdvertQueries;
public sealed record GetMyAdvertsQuery(string Email) : IRequest<Pagination<MinimalInformationsAboutAdvertDto>>;