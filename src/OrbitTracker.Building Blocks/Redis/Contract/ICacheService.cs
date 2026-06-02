namespace OrbitTracker.Building_Blocks.Redis.Contract;

public interface ICacheService
{
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default);
    
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
    
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}