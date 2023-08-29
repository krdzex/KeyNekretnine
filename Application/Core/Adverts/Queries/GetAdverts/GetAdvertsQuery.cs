using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdverts;
public sealed record GetAdvertsQuery(AdvertParameters AdvertParameters) : IQuery<Pagination<MinimalInformationsAboutAdvertDto>>;