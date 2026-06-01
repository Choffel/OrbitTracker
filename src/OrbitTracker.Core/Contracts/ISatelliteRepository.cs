using OrbitTracker.Core.Models;

namespace OrbitTracker.Core.Contracts;

public interface ISatelliteRepository
{
    Task SavePositionAsync(SatellitePosition position, CancellationToken cancellationToken);
    
    Task<SatellitePosition?> GetLAstPositionAsync(CancellationToken cancellationToken);
}