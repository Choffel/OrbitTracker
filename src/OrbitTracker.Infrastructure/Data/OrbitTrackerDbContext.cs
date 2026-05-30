using Microsoft.EntityFrameworkCore;
using OrbitTracker.Core.Models;

namespace OrbitTracker.Infrastructure.Data;

public class OrbitTrackerDbContext : DbContext
{
    public DbSet<SatellitePosition> SatellitePositions { get; set; } = null!;

    public OrbitTrackerDbContext(DbContextOptions<OrbitTrackerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SatellitePosition>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Latitude)
                .HasPrecision(10, 8);
            
            entity.Property(e => e.Longitude)
                .HasPrecision(11, 8);
            
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.ToTable("satellite_positions");
        });
    }
}

