using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace Application.Core.Adverts.Queries.GetADvertsCompare;
public sealed record GetAdvertsCompareQuery(int FirstAdvert, int SacondAdvert) : IQuery<List<CompareAdvertDto>>;