using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Application.Behaviors;
public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>, ICacheableMediatrQuery
{
    private readonly IDistributedCache _cache;
    private readonly ILogger _logger;
    public CachingBehavior(IDistributedCache cache, ILogger<TResponse> logger)
    {
        _cache = cache;
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response;
        if (request.BypassCache) return await next();
        async Task<TResponse> GetResponseAndAddToCache()
        {
            response = await next();
            var slidingExpiration = request.SlidingExpiration == null ? TimeSpan.FromHours(2) : request.SlidingExpiration;
            var options = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration };
            var serializedData = JsonConvert.SerializeObject(response);
            await _cache.SetStringAsync((string)request.CacheKey, serializedData, options, cancellationToken);
            return response;
        }
        var cachedResponse = await _cache.GetStringAsync((string)request.CacheKey, cancellationToken);
        if (cachedResponse != null)
        {
            response = JsonSerializer.Deserialize<TResponse>(cachedResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            _logger.LogInformation($"Fetched from Cache -> '{request.CacheKey}'.");
        }
        else
        {
            response = await GetResponseAndAddToCache();
            _logger.LogInformation($"Added to Cache -> '{request.CacheKey}'.");
        }
        return response;
    }
}
