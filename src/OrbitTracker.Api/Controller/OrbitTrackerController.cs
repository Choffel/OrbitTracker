using Microsoft.AspNetCore.Mvc;
using OrbitTracker.Core.BackgroundService;
using OrbitTracker.Core.Contracts;

namespace OrbitTracker.Api.Controller;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OrbitTrackerController : ControllerBase
{
    private readonly ISatelliteTrackingService _trackingService;
    private readonly ISatelliteRepository _repository;
    
    public OrbitTrackerController(ISatelliteTrackingService trackingService, ISatelliteRepository repository)
    {
        _repository = repository;
        _trackingService = trackingService;
    }
    
    [HttpGet("get-positions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCurrentPosition(CancellationToken cancellationToken)
    {
        var position = await _repository.GetLAstPositionAsync(cancellationToken);
        return Ok(position);
    }
}