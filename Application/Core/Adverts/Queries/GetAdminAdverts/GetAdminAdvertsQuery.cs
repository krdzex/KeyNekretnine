using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdminAdverts;
public sealed record GetAdminAdvertsQuery(AdminAdvertParameters AdminAdvertParameters) : IQuery<Pagination<AdminTableAdvertDto>>;