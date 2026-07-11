using ApiToolKit.Application.DTOs.Maps.DistanceMatrix;
using ApiToolKit.Application.Interfaces.Services.Maps;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Maps;

/// <summary>
/// Distance Matrix endpoints for the Google Maps Platform.
/// </summary>
[ApiController]
[Route("api/maps/distance-matrix")]
public class MapsDistanceMatrixController : ControllerBase
{
    private readonly IMapsDistanceMatrixService _distanceMatrixService;

    public MapsDistanceMatrixController(IMapsDistanceMatrixService distanceMatrixService)
    {
        _distanceMatrixService = distanceMatrixService;
    }

    /// <summary>
    /// Calculate travel distance and time for a matrix of origins and destinations.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetDistanceMatrixAsync([FromQuery] DistanceMatrixRequest request)
    {
        var result = await _distanceMatrixService.GetDistanceMatrixAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
