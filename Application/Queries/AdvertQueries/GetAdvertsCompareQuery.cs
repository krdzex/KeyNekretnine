using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Queries.AdvertQueries;
public sealed record GetAdvertsCompareQuery(int FirstAdvert, int SacondAdvert) : IRequest<IEnumerable<CompareAdvertDto>>;