using ApiToolKit.Application.DTOs.DisplayVideo.TargetingTypeAssignedTargetingOption;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// targetingTypes / assignedTargetingOptions (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoTargetingTypeAssignedTargetingOptionController : ControllerBase
{
    private readonly IDisplayVideoTargetingTypeAssignedTargetingOptionService _targetingTypeAssignedTargetingOptionService;

    public DisplayVideoTargetingTypeAssignedTargetingOptionController(IDisplayVideoTargetingTypeAssignedTargetingOptionService targetingTypeAssignedTargetingOptionService)
    {
        _targetingTypeAssignedTargetingOptionService = targetingTypeAssignedTargetingOptionService;
    }

    /// <summary>Lists the targeting options assigned to an advertiser.</summary>
    [HttpGet("advertisers/{advertiserId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions")]
    public async Task<IActionResult> ListAssignedTargetingOptionsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string targetingTypeId, [FromQuery] string? orderBy = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null, [FromQuery] string? filter = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAssignedTargetingOptionsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, TargetingTypeId = targetingTypeId, OrderBy = orderBy, PageSize = pageSize, PageToken = pageToken, Filter = filter };
        var result = await _targetingTypeAssignedTargetingOptionService.ListAssignedTargetingOptionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Assigns a targeting option to an advertiser.</summary>
    [HttpPost("advertisers/{advertiserId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions")]
    public async Task<IActionResult> CreateAssignedTargetingOptionAsync([FromRoute] string advertiserId, [FromRoute] string targetingTypeId, [FromBody] CreateAssignedTargetingOptionRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.TargetingTypeId = targetingTypeId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _targetingTypeAssignedTargetingOptionService.CreateAssignedTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an assigned targeting option from an advertiser.</summary>
    [HttpDelete("advertisers/{advertiserId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions/{assignedTargetingOptionId}")]
    public async Task<IActionResult> DeleteAssignedTargetingOptionAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string targetingTypeId, [FromRoute] string assignedTargetingOptionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteAssignedTargetingOptionRequest { AccountKey = accountKey, AdvertiserId = advertiserId, TargetingTypeId = targetingTypeId, AssignedTargetingOptionId = assignedTargetingOptionId };
        var result = await _targetingTypeAssignedTargetingOptionService.DeleteAssignedTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a single targeting option assigned to an advertiser.</summary>
    [HttpGet("advertisers/{advertiserId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions/{assignedTargetingOptionId}")]
    public async Task<IActionResult> GetAssignedTargetingOptionAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string targetingTypeId, [FromRoute] string assignedTargetingOptionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAssignedTargetingOptionRequest { AccountKey = accountKey, AdvertiserId = advertiserId, TargetingTypeId = targetingTypeId, AssignedTargetingOptionId = assignedTargetingOptionId };
        var result = await _targetingTypeAssignedTargetingOptionService.GetAssignedTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
