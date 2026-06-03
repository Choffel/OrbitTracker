using OrbitTracker.Core.Models;

namespace OrbitTracker.Core.Contracts;

public interface ISatelliteCacheService
{
    Task<SatellitePosition?> GetCachedPositionAsync(CancellationToken cancellationToken);
    
    Task CachePositionAsync(SatellitePosition position, CancellationToken cancellationToken);
}