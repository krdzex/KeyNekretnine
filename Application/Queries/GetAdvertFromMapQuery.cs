using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Queries;
public sealed record GetAdvertFromMapQuery(int Id) : IRequest<MinimalInformationsAboutAdvertDto>;

