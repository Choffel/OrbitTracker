using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrbitTracker.Core.Contracts;

namespace OrbitTracker.Core.BackgroundService;

public class SatteliteTrackerWorker : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SatteliteTrackerWorker> _logger;

    
    public SatteliteTrackerWorker(IServiceProvider serviceProvider, ILogger<SatteliteTrackerWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Woker стартовал...");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var trackingService = scope.ServiceProvider.GetRequiredService<ISatelliteTrackingService>();
                    var repository = scope.ServiceProvider.GetRequiredService<ISatelliteRepository>();
                    
                    
                    var cacheService = scope.ServiceProvider.GetRequiredService<ISatelliteCacheService>();

                    _logger.LogInformation("Запрашиваем координаты спутника...");
                    var position = await trackingService.TrackSatelliteAsync(stoppingToken);

                    _logger.LogInformation("Сохраняем позицию в базу данных...");
                    await repository.SavePositionAsync(position, stoppingToken);
                    
                    _logger.LogInformation("Обновляем актуальную позицию в кэше Redis...");
                    await cacheService.CachePositionAsync(position, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка во время выполнения итерации воркера.");
            }

            
            await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
        }
    }
}