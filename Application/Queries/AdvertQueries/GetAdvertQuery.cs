using Application.Behaviors;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Queries.AdvertQuery;
public sealed record GetAdvertQuery : IRequest<AllInfomrationsAboutAdvertDto>, ICacheableMediatrQuery
{
    public int Id { get; set; }
    public bool BypassCache { get; set; }
    public string CacheKey => $"Advert-{Id}";
    public TimeSpan? SlidingExpiration { get; set; }
};

