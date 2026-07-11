using ApiToolKit.Application.DTOs.Maps.StaticMap;
using ApiToolKit.Application.Interfaces.Services.Maps;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Maps;

/// <summary>
/// Static Maps endpoints for the Google Maps Platform.
/// </summary>
[ApiController]
[Route("api/maps/static")]
public class MapsStaticController : ControllerBase
{
    private readonly IMapsStaticMapService _staticMapService;

    public MapsStaticController(IMapsStaticMapService staticMapService)
    {
        _staticMapService = staticMapService;
    }

    /// <summary>
    /// Generate a static map image URL for a given center, zoom, and size.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetStaticMapUrlAsync([FromQuery] StaticMapRequest request)
    {
        var result = await _staticMapService.GetStaticMapUrlAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
