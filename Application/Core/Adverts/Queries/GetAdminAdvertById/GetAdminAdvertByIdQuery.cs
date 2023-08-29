using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdminAdvertById;
public sealed record GetAdminAdvertByIdQuery(int AdvertId) : IQuery<AdminAllInformationsAboutAdvertDto>;