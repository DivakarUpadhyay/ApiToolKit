using ApiToolKit.Application.DTOs.DisplayVideo.LocationList;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// locationLists (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoLocationListController : ControllerBase
{
    private readonly IDisplayVideoLocationListService _locationListService;

    public DisplayVideoLocationListController(IDisplayVideoLocationListService locationListService)
    {
        _locationListService = locationListService;
    }

    /// <summary>Updates a location list.</summary>
    [HttpPatch("advertisers/{advertiserId}/locationLists/{locationListId}")]
    public async Task<IActionResult> UpdateLocationListAsync([FromRoute] string advertiserId, [FromRoute] string locationListId, [FromBody] UpdateLocationListRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.LocationListId = locationListId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationListService.UpdateLocationListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a location list.</summary>
    [HttpGet("advertisers/{advertiserId}/locationLists/{locationListId}")]
    public async Task<IActionResult> GetLocationListAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string locationListId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetLocationListRequest { AccountKey = accountKey, AdvertiserId = advertiserId, LocationListId = locationListId };
        var result = await _locationListService.GetLocationListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists location lists based on a given advertiser id.</summary>
    [HttpGet("advertisers/{advertiserId}/locationLists")]
    public async Task<IActionResult> ListLocationListsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] string? filter = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null, [FromQuery] string? orderBy = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListLocationListsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, Filter = filter, PageSize = pageSize, PageToken = pageToken, OrderBy = orderBy };
        var result = await _locationListService.ListLocationListsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new location list.</summary>
    [HttpPost("advertisers/{advertiserId}/locationLists")]
    public async Task<IActionResult> CreateLocationListAsync([FromRoute] string advertiserId, [FromBody] CreateLocationListRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationListService.CreateLocationListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
