using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace Application.Core.Adverts.Queries.GetAdminAdvert;
public sealed record GetAdminAdvertByIdQuery(int AdvertId) : IQuery<AdminAllInformationsAboutAdvertDto>;
