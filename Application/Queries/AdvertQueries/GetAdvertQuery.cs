using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Queries.AdvertQuery;
public sealed record GetAdvertQuery(int Id) : IRequest<AllInfomrationsAboutAdvertDto>;

