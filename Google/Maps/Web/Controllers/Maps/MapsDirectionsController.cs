using ApiToolKit.Application.DTOs.Maps.Directions;
using ApiToolKit.Application.Interfaces.Services.Maps;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Maps;

/// <summary>
/// Directions endpoints for the Google Maps Platform.
/// </summary>
[ApiController]
[Route("api/maps/directions")]
public class MapsDirectionsController : ControllerBase
{
    private readonly IMapsDirectionsService _directionsService;

    public MapsDirectionsController(IMapsDirectionsService directionsService)
    {
        _directionsService = directionsService;
    }

    /// <summary>
    /// Get directions between two locations with optional waypoints.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetDirectionsAsync([FromQuery] DirectionsRequest request)
    {
        var result = await _directionsService.GetDirectionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
