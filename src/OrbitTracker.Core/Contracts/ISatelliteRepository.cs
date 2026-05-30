using OrbitTracker.Core.Models;

namespace OrbitTracker.Core.Contracts;

public interface ISatelliteRepository
{
    Task SavePositionAsync(SatellitePosition position);
    
    Task<SatellitePosition?> GetLAstPositionAsync(CancellationToken cancellationToken);
}