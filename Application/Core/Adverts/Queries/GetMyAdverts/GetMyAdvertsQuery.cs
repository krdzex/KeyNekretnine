using Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Application.Core.Adverts.Queries.GetMyAdverts;
public sealed record GetMyAdvertsQuery(MyAdvertsParameters MyAdvertParameters, string Email) : IQuery<Pagination<MyAdvertsDto>>;