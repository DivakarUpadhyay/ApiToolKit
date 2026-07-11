using ApiToolKit.Application.DTOs.Maps.Places;
using ApiToolKit.Application.Interfaces.Services.Maps;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Maps;

/// <summary>
/// Places endpoints for the Google Maps Platform.
/// </summary>
[ApiController]
[Route("api/maps/places")]
public class MapsPlacesController : ControllerBase
{
    private readonly IMapsPlacesService _placesService;

    public MapsPlacesController(IMapsPlacesService placesService)
    {
        _placesService = placesService;
    }

    /// <summary>
    /// Search for places within a specified radius of a location.
    /// </summary>
    [HttpGet("nearby")]
    public async Task<IActionResult> NearbySearchAsync([FromQuery] NearbySearchRequest request)
    {
        var result = await _placesService.NearbySearchAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Search for places based on a text query.
    /// </summary>
    [HttpGet("search")]
    public async Task<IActionResult> TextSearchAsync([FromQuery] TextSearchRequest request)
    {
        var result = await _placesService.TextSearchAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Get detailed information about a specific place by its PlaceId.
    /// </summary>
    [HttpGet("details")]
    public async Task<IActionResult> GetPlaceDetailsAsync([FromQuery] PlaceDetailsRequest request)
    {
        var result = await _placesService.GetPlaceDetailsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Get place autocomplete predictions for an input string.
    /// </summary>
    [HttpGet("autocomplete")]
    public async Task<IActionResult> AutoCompleteAsync([FromQuery] PlaceAutoCompleteRequest request)
    {
        var result = await _placesService.AutoCompleteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Build a URL to retrieve a place photo by photo reference.
    /// </summary>
    [HttpGet("photo")]
    public async Task<IActionResult> GetPlacePhotoAsync([FromQuery] string photoReference, [FromQuery] int maxWidth = 400)
    {
        var result = await _placesService.GetPlacePhotoAsync(photoReference, maxWidth);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
