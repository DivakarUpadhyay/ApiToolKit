using ApiToolKit.Application.DTOs.Maps.Roads;
using ApiToolKit.Application.Interfaces.Services.Maps;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Maps;

/// <summary>
/// Roads endpoints for the Google Maps Platform.
/// </summary>
[ApiController]
[Route("api/maps/roads")]
public class MapsRoadsController : ControllerBase
{
    private readonly IMapsRoadsService _roadsService;

    public MapsRoadsController(IMapsRoadsService roadsService)
    {
        _roadsService = roadsService;
    }

    /// <summary>
    /// Snap GPS coordinates to the nearest roads.
    /// </summary>
    [HttpGet("snap")]
    public async Task<IActionResult> SnapToRoadsAsync([FromQuery] SnapToRoadsRequest request)
    {
        var result = await _roadsService.SnapToRoadsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
