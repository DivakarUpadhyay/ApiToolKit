using ApiToolKit.Application.DTOs.DisplayVideo.AdGroup;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// adGroups (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoAdGroupController : ControllerBase
{
    private readonly IDisplayVideoAdGroupService _adGroupService;

    public DisplayVideoAdGroupController(IDisplayVideoAdGroupService adGroupService)
    {
        _adGroupService = adGroupService;
    }

    /// <summary>Bulk edits targeting options for multiple ad groups.</summary>
    [HttpPost("advertisers/{advertiserId}/adGroups/bulkEditAssignedTargetingOptions")]
    public async Task<IActionResult> BulkEditAssignedTargetingOptionsAdGroupAsync([FromRoute] string advertiserId, [FromBody] BulkEditAssignedTargetingOptionsAdGroupRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adGroupService.BulkEditAssignedTargetingOptionsAdGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists assigned targeting options for multiple ad groups across targeting types.</summary>
    [HttpGet("advertisers/{advertiserId}/adGroups/bulkListAssignedTargetingOptions")]
    public async Task<IActionResult> BulkListAssignedTargetingOptionsAdGroupAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] string? orderBy = null, [FromQuery] List<string>? adGroupIds = null, [FromQuery] string? pageToken = null, [FromQuery] string? filter = null, [FromQuery] int? pageSize = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new BulkListAssignedTargetingOptionsAdGroupRequest { AccountKey = accountKey, AdvertiserId = advertiserId, OrderBy = orderBy, AdGroupIds = adGroupIds, PageToken = pageToken, Filter = filter, PageSize = pageSize };
        var result = await _adGroupService.BulkListAssignedTargetingOptionsAdGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists ad groups.</summary>
    [HttpGet("advertisers/{advertiserId}/adGroups")]
    public async Task<IActionResult> ListAdGroupsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null, [FromQuery] string? orderBy = null, [FromQuery] string? filter = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAdGroupsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, PageSize = pageSize, PageToken = pageToken, OrderBy = orderBy, Filter = filter };
        var result = await _adGroupService.ListAdGroupsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a AdGroup.</summary>
    [HttpDelete("advertisers/{advertiserId}/adGroups/{adGroupId}")]
    public async Task<IActionResult> DeleteAdGroupAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string adGroupId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteAdGroupRequest { AccountKey = accountKey, AdvertiserId = advertiserId, AdGroupId = adGroupId };
        var result = await _adGroupService.DeleteAdGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets an ad group.</summary>
    [HttpGet("advertisers/{advertiserId}/adGroups/{adGroupId}")]
    public async Task<IActionResult> GetAdGroupAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string adGroupId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAdGroupRequest { AccountKey = accountKey, AdvertiserId = advertiserId, AdGroupId = adGroupId };
        var result = await _adGroupService.GetAdGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new ad group.</summary>
    [HttpPost("advertisers/{advertiserId}/adGroups")]
    public async Task<IActionResult> CreateAdGroupAsync([FromRoute] string advertiserId, [FromBody] CreateAdGroupRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adGroupService.CreateAdGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing ad group.</summary>
    [HttpPatch("advertisers/{advertiserId}/adGroups/{adGroupId}")]
    public async Task<IActionResult> UpdateAdGroupAsync([FromRoute] string advertiserId, [FromRoute] string adGroupId, [FromBody] UpdateAdGroupRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.AdGroupId = adGroupId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adGroupService.UpdateAdGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
