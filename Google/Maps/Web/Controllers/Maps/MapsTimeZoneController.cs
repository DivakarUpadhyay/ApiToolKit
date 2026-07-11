using ApiToolKit.Application.DTOs.Maps.TimeZone;
using ApiToolKit.Application.Interfaces.Services.Maps;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Maps;

/// <summary>
/// Time Zone endpoints for the Google Maps Platform.
/// </summary>
[ApiController]
[Route("api/maps/timezone")]
public class MapsTimeZoneController : ControllerBase
{
    private readonly IMapsTimeZoneService _timeZoneService;

    public MapsTimeZoneController(IMapsTimeZoneService timeZoneService)
    {
        _timeZoneService = timeZoneService;
    }

    /// <summary>
    /// Get time zone information for a given location and timestamp.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetTimeZoneAsync([FromQuery] TimeZoneRequest request)
    {
        var result = await _timeZoneService.GetTimeZoneAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
