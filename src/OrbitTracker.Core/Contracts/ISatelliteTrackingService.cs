using OrbitTracker.Core.Models;
using OrbitTracker.Infrastructure.DtosForService;

namespace OrbitTracker.Core.Contracts;

public interface ISatelliteTrackingService
{
    Task<SatellitePosition> TrackSatelliteAsync(CancellationToken cancellationToken);
}