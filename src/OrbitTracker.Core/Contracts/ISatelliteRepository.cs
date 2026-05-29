using OrbitTracker.Core.Models;

namespace OrbitTracker.Core.Contracts;

public interface ISatelliteRepository
{
    Task AddAsync(SatellitePosition position);
}