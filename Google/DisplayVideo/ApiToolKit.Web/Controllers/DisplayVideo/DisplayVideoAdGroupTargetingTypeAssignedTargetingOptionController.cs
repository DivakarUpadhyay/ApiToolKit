using ApiToolKit.Application.DTOs.DisplayVideo.AdGroupTargetingTypeAssignedTargetingOption;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// adGroups / targetingTypes / assignedTargetingOptions (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoAdGroupTargetingTypeAssignedTargetingOptionController : ControllerBase
{
    private readonly IDisplayVideoAdGroupTargetingTypeAssignedTargetingOptionService _adGroupTargetingTypeAssignedTargetingOptionService;

    public DisplayVideoAdGroupTargetingTypeAssignedTargetingOptionController(IDisplayVideoAdGroupTargetingTypeAssignedTargetingOptionService adGroupTargetingTypeAssignedTargetingOptionService)
    {
        _adGroupTargetingTypeAssignedTargetingOptionService = adGroupTargetingTypeAssignedTargetingOptionService;
    }

    /// <summary>Lists the targeting options assigned to an ad group.</summary>
    [HttpGet("advertisers/{advertiserId}/adGroups/{adGroupId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions")]
    public async Task<IActionResult> ListAssignedTargetingOptionsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string adGroupId, [FromRoute] string targetingTypeId, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null, [FromQuery] string? filter = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAssignedTargetingOptionsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, AdGroupId = adGroupId, TargetingTypeId = targetingTypeId, PageSize = pageSize, OrderBy = orderBy, Filter = filter, PageToken = pageToken };
        var result = await _adGroupTargetingTypeAssignedTargetingOptionService.ListAssignedTargetingOptionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a single targeting option assigned to an ad group.</summary>
    [HttpGet("advertisers/{advertiserId}/adGroups/{adGroupId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions/{assignedTargetingOptionId}")]
    public async Task<IActionResult> GetAssignedTargetingOptionAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string adGroupId, [FromRoute] string targetingTypeId, [FromRoute] string assignedTargetingOptionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAssignedTargetingOptionRequest { AccountKey = accountKey, AdvertiserId = advertiserId, AdGroupId = adGroupId, TargetingTypeId = targetingTypeId, AssignedTargetingOptionId = assignedTargetingOptionId };
        var result = await _adGroupTargetingTypeAssignedTargetingOptionService.GetAssignedTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Assigns a targeting option to an ad group.</summary>
    [HttpPost("advertisers/{advertiserId}/adGroups/{adGroupId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions")]
    public async Task<IActionResult> CreateAssignedTargetingOptionAsync([FromRoute] string advertiserId, [FromRoute] string adGroupId, [FromRoute] string targetingTypeId, [FromBody] CreateAssignedTargetingOptionRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.AdGroupId = adGroupId;
        request.TargetingTypeId = targetingTypeId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adGroupTargetingTypeAssignedTargetingOptionService.CreateAssignedTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an assigned targeting option from an ad group.</summary>
    [HttpDelete("advertisers/{advertiserId}/adGroups/{adGroupId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions/{assignedTargetingOptionId}")]
    public async Task<IActionResult> DeleteAssignedTargetingOptionAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string adGroupId, [FromRoute] string targetingTypeId, [FromRoute] string assignedTargetingOptionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteAssignedTargetingOptionRequest { AccountKey = accountKey, AdvertiserId = advertiserId, AdGroupId = adGroupId, TargetingTypeId = targetingTypeId, AssignedTargetingOptionId = assignedTargetingOptionId };
        var result = await _adGroupTargetingTypeAssignedTargetingOptionService.DeleteAssignedTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
