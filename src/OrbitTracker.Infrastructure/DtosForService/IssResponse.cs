namespace OrbitTracker.Infrastructure.DtosForService;

public record IssResponse(
    double Latitude,
    double Longitude,
    long Timestamp
    );