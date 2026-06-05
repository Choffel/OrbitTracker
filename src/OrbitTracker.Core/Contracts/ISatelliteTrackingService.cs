using OrbitTracker.Core.Models;

namespace OrbitTracker.Core.Contracts;

public interface ISatelliteTrackingService
{
    Task<SatellitePosition> TrackSatelliteAsync(CancellationToken cancellationToken);
}