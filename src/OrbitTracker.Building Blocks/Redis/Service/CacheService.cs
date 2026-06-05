using System.Text.Json;
using OrbitTracker.Building_Blocks.Redis.Contract;
using StackExchange.Redis;

namespace OrbitTracker.Building_Blocks.Redis.Service;

public class CacheService : ICacheService
{
    private readonly IDatabase _database;
    
    private const string CacheKeyPrefix = "OrbitTrackerCache:";
    
    public CacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
    }
    
    //fix 
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        string fullKey = CacheKeyPrefix + key;
        
        string jsonStr = JsonSerializer.Serialize(value);
        
        TimeSpan actualExpiry = expiry ?? TimeSpan.FromMinutes(5);
        
        await _database.StringSetAsync(fullKey, jsonStr, actualExpiry);
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        string fullKey = CacheKeyPrefix + key;
        
        RedisValue cachedValue = _database.StringGet(fullKey);

        if (!cachedValue.HasValue)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(cachedValue.ToString());
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        string fullKey = CacheKeyPrefix + key;
        await _database.KeyDeleteAsync(fullKey);
    }
}