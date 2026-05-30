using Microsoft.EntityFrameworkCore;
using OrbitTracker.Core.Contracts;
using OrbitTracker.Core.Models;
using OrbitTracker.Infrastructure.Data;

namespace OrbitTracker.Infrastructure.Repository;

public class SatelliteRepository : ISatelliteRepository
{
    private readonly OrbitTrackerDbContext _dbContext;
    
    public SatelliteRepository(OrbitTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task SavePositionAsync(SatellitePosition position)
    {
        position.Id = Guid.NewGuid();
        _dbContext.SatellitePositions.Add(position);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<SatellitePosition?> GetLAstPositionAsync(CancellationToken cancellationToken)
    {
        var lastPosition = await _dbContext.SatellitePositions
            .OrderByDescending(p => p.TimeStamp)
            .FirstOrDefaultAsync(cancellationToken);
        return lastPosition;
    }
}