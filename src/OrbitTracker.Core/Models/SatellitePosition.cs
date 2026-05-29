namespace OrbitTracker.Core.Models;

public class SatellitePosition
{
    public Guid Id { get; set; }
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    
    public DateTime TimeStamp { get; set; }
}