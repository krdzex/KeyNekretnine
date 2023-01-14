using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Queries;
public sealed record GetAdvertQuery(int Id) : IRequest<AllInfomrationsAboutAdvertDto>;

