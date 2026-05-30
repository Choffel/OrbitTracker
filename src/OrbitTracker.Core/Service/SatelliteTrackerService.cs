using System.Net.Http.Json;
using OrbitTracker.Core.Contracts;
using OrbitTracker.Core.Models;
using OrbitTracker.Infrastructure.DtosForService;

namespace OrbitTracker.Core.Service;

public class SatelliteTrackerService : ISatelliteTrackingService
{
    private string url = "http://api.open-notify.org/iss-now.json";
    private readonly HttpClient client;
    
    
    public async Task<SatellitePosition> TrackSatelliteAsync(CancellationToken cancellationToken)
    {
        var response = await client.GetFromJsonAsync<IssResponse>(url, cancellationToken);

        if (response == null)
        {
            throw new Exception("Failed to retrieve satellite position.");
        }
    
        return new SatellitePosition
        {
            Latitude = response.Latitude,
            Longitude = response.Longitude,
            TimeStamp = DateTimeOffset.FromUnixTimeSeconds(response.Timestamp).DateTime
        };
    }
}