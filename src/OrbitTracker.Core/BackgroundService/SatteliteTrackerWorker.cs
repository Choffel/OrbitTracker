using OrbitTracker.Core.Contracts;

namespace OrbitTracker.Core.BackgroundService;

public class SatteliteTrackerWorker : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly ISatelliteRepository _satelliteRepository;
    private readonly ISatelliteTrackingService _trackerService;

    public SatteliteTrackerWorker(ISatelliteTrackingService trackerService, ISatelliteRepository satelliteRepository)
    {
        _satelliteRepository = satelliteRepository;
        _trackerService = trackerService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var currentPosition = await _trackerService.TrackSatelliteAsync(stoppingToken);
                await _satelliteRepository.SavePositionAsync(currentPosition);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}