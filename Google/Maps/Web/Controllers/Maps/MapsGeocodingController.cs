using ApiToolKit.Application.DTOs.Maps.Geocoding;
using ApiToolKit.Application.Interfaces.Services.Maps;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Maps;

/// <summary>
/// Geocoding and Reverse Geocoding endpoints for the Google Maps Platform.
/// </summary>
[ApiController]
[Route("api/maps/geocode")]
public class MapsGeocodingController : ControllerBase
{
    private readonly IMapsGeocodingService _geocodingService;

    public MapsGeocodingController(IMapsGeocodingService geocodingService)
    {
        _geocodingService = geocodingService;
    }

    /// <summary>
    /// Geocode an address to latitude/longitude coordinates.
    /// </summary>
    [HttpGet("address")]
    public async Task<IActionResult> GeocodeAddressAsync([FromQuery] GeocodeAddressRequest request)
    {
        var result = await _geocodingService.GeocodeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Reverse geocode latitude/longitude coordinates to an address.
    /// </summary>
    [HttpGet("reverse")]
    public async Task<IActionResult> ReverseGeocodeAsync([FromQuery] ReverseGeocodeRequest request)
    {
        var result = await _geocodingService.ReverseGeocodeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
