using Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Application.Core.Adverts.Queries.GetAdminAdverts;
public sealed record GetAdminAdvertsQuery(AdminAdvertParameters AdminAdvertParameters) : IQuery<Pagination<AdminTableAdvertDto>>;