using ApiToolKit.Application.DTOs.DisplayVideo.LocationListAssignedLocation;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// locationLists / assignedLocations (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoLocationListAssignedLocationController : ControllerBase
{
    private readonly IDisplayVideoLocationListAssignedLocationService _locationListAssignedLocationService;

    public DisplayVideoLocationListAssignedLocationController(IDisplayVideoLocationListAssignedLocationService locationListAssignedLocationService)
    {
        _locationListAssignedLocationService = locationListAssignedLocationService;
    }

    /// <summary>Lists locations assigned to a location list.</summary>
    [HttpGet("advertisers/{advertiserId}/locationLists/{locationListId}/assignedLocations")]
    public async Task<IActionResult> ListAssignedLocationsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string locationListId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null, [FromQuery] string? orderBy = null, [FromQuery] string? filter = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAssignedLocationsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, LocationListId = locationListId, PageSize = pageSize, PageToken = pageToken, OrderBy = orderBy, Filter = filter };
        var result = await _locationListAssignedLocationService.ListAssignedLocationsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates an assignment between a location and a location list.</summary>
    [HttpPost("advertisers/{advertiserId}/locationLists/{locationListId}/assignedLocations")]
    public async Task<IActionResult> CreateAssignedLocationAsync([FromRoute] string advertiserId, [FromRoute] string locationListId, [FromBody] CreateAssignedLocationRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.LocationListId = locationListId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationListAssignedLocationService.CreateAssignedLocationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes the assignment between a location and a location list.</summary>
    [HttpDelete("advertisers/{advertiserId}/locationLists/{locationListId}/assignedLocations/{assignedLocationId}")]
    public async Task<IActionResult> DeleteAssignedLocationAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string locationListId, [FromRoute] string assignedLocationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteAssignedLocationRequest { AccountKey = accountKey, AdvertiserId = advertiserId, LocationListId = locationListId, AssignedLocationId = assignedLocationId };
        var result = await _locationListAssignedLocationService.DeleteAssignedLocationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Bulk edits multiple assignments between locations and a single location list.</summary>
    [HttpPost("advertisers/{advertiserId}/locationLists/{locationListId}/assignedLocations/bulkEdit")]
    public async Task<IActionResult> BulkEditAssignedLocationAsync([FromRoute] string advertiserId, [FromRoute] string locationListId, [FromBody] BulkEditAssignedLocationRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.LocationListId = locationListId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationListAssignedLocationService.BulkEditAssignedLocationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
