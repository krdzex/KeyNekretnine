using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Application.Queries.AdvertQueries;
public sealed record GetMyAdvertsQuery(AdvertParameters AdvertParameters, string Email) : IRequest<Pagination<MinimalInformationsAboutAdvertDto>>;