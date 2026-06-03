using OrbitTracker.Building_Blocks.Redis.Contract;
using OrbitTracker.Core.Contracts;
using OrbitTracker.Core.Models;

namespace OrbitTracker.Infrastructure.Caching;

public class RedisSateliteCacheService : ISatelliteCacheService
{
    private readonly ICacheService _cacheService;
    
    private const string CacheKey = "satellite:iss:current";
    
    public RedisSateliteCacheService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }
    
    public async Task CachePositionAsync(SatellitePosition position, CancellationToken cancellationToken)
    {
        await _cacheService.SetAsync(CacheKey, position, cancellationToken: cancellationToken);
    }

    
    public async Task<SatellitePosition?> GetCachedPositionAsync(CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<SatellitePosition>(CacheKey, cancellationToken);
    }
}