using ApiToolKit.Application.DTOs.DisplayVideo.LineItemTargetingTypeAssignedTargetingOption;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// lineItems / targetingTypes / assignedTargetingOptions (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoLineItemTargetingTypeAssignedTargetingOptionController : ControllerBase
{
    private readonly IDisplayVideoLineItemTargetingTypeAssignedTargetingOptionService _lineItemTargetingTypeAssignedTargetingOptionService;

    public DisplayVideoLineItemTargetingTypeAssignedTargetingOptionController(IDisplayVideoLineItemTargetingTypeAssignedTargetingOptionService lineItemTargetingTypeAssignedTargetingOptionService)
    {
        _lineItemTargetingTypeAssignedTargetingOptionService = lineItemTargetingTypeAssignedTargetingOptionService;
    }

    /// <summary>Gets a single targeting option assigned to a line item.</summary>
    [HttpGet("advertisers/{advertiserId}/lineItems/{lineItemId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions/{assignedTargetingOptionId}")]
    public async Task<IActionResult> GetAssignedTargetingOptionAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string lineItemId, [FromRoute] string targetingTypeId, [FromRoute] string assignedTargetingOptionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAssignedTargetingOptionRequest { AccountKey = accountKey, AdvertiserId = advertiserId, LineItemId = lineItemId, TargetingTypeId = targetingTypeId, AssignedTargetingOptionId = assignedTargetingOptionId };
        var result = await _lineItemTargetingTypeAssignedTargetingOptionService.GetAssignedTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists the targeting options assigned to a line item.</summary>
    [HttpGet("advertisers/{advertiserId}/lineItems/{lineItemId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions")]
    public async Task<IActionResult> ListAssignedTargetingOptionsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string lineItemId, [FromRoute] string targetingTypeId, [FromQuery] string? orderBy = null, [FromQuery] int? pageSize = null, [FromQuery] string? filter = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAssignedTargetingOptionsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, LineItemId = lineItemId, TargetingTypeId = targetingTypeId, OrderBy = orderBy, PageSize = pageSize, Filter = filter, PageToken = pageToken };
        var result = await _lineItemTargetingTypeAssignedTargetingOptionService.ListAssignedTargetingOptionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Assigns a targeting option to a line item.</summary>
    [HttpPost("advertisers/{advertiserId}/lineItems/{lineItemId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions")]
    public async Task<IActionResult> CreateAssignedTargetingOptionAsync([FromRoute] string advertiserId, [FromRoute] string lineItemId, [FromRoute] string targetingTypeId, [FromBody] CreateAssignedTargetingOptionRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.LineItemId = lineItemId;
        request.TargetingTypeId = targetingTypeId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _lineItemTargetingTypeAssignedTargetingOptionService.CreateAssignedTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an assigned targeting option from a line item.</summary>
    [HttpDelete("advertisers/{advertiserId}/lineItems/{lineItemId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions/{assignedTargetingOptionId}")]
    public async Task<IActionResult> DeleteAssignedTargetingOptionAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string lineItemId, [FromRoute] string targetingTypeId, [FromRoute] string assignedTargetingOptionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteAssignedTargetingOptionRequest { AccountKey = accountKey, AdvertiserId = advertiserId, LineItemId = lineItemId, TargetingTypeId = targetingTypeId, AssignedTargetingOptionId = assignedTargetingOptionId };
        var result = await _lineItemTargetingTypeAssignedTargetingOptionService.DeleteAssignedTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
