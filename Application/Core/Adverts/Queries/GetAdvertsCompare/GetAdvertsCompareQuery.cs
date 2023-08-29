using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertsCompare;
public sealed record GetAdvertsCompareQuery(int FirstAdvert, int SacondAdvert) : IQuery<List<CompareAdvertDto>>;