using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Application.Queries.AdvertQuery;
public sealed record GetAdvertsQuery(AdvertParameters AdvertParameters) : IRequest<Pagination<MinimalInformationsAboutAdvertDto>>;
