using ApiToolKit.Application.DTOs.Maps.Elevation;
using ApiToolKit.Application.Interfaces.Services.Maps;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Maps;

/// <summary>
/// Elevation endpoints for the Google Maps Platform.
/// </summary>
[ApiController]
[Route("api/maps/elevation")]
public class MapsElevationController : ControllerBase
{
    private readonly IMapsElevationService _elevationService;

    public MapsElevationController(IMapsElevationService elevationService)
    {
        _elevationService = elevationService;
    }

    /// <summary>
    /// Get elevation data for one or more locations.
    /// </summary>
    [HttpGet("locations")]
    public async Task<IActionResult> GetElevationAsync([FromQuery] ElevationRequest request)
    {
        var result = await _elevationService.GetElevationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Get elevation data along a path with sampled points.
    /// </summary>
    [HttpGet("path")]
    public async Task<IActionResult> GetElevationAlongPathAsync([FromQuery] ElevationPathRequest request)
    {
        var result = await _elevationService.GetElevationAlongPathAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
