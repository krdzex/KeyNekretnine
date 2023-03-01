using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Queries.AdvertQueries;
public sealed record GetAdminAdvertQuery(int AdvertId) : IRequest<AdminAllInformationsAboutAdvertDto>;
