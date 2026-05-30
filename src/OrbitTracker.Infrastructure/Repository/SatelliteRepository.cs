using OrbitTracker.Core.Contracts;
using OrbitTracker.Core.Models;

namespace OrbitTracker.Infrastructure.Repository;

public class SatelliteRepository : ISatelliteRepository
{
    private readonly List<SatellitePosition> _positions = new();
    
    public Task SavePositionAsync(SatellitePosition position)
    {
        position.Id = Guid.NewGuid();
        _positions.Add(position);
        return Task.CompletedTask;
    }

    public Task<SatellitePosition?> GetLAstPositionAsync(CancellationToken cancellationToken)
    {
        var lastPosition = _positions.LastOrDefault();
        return Task.FromResult(lastPosition);
    }
}