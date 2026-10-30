using ApiToolKit.Application.DTOs.DisplayVideo.PartnerTargetingTypeAssignedTargetingOption;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// partners / targetingTypes / assignedTargetingOptions (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoPartnerTargetingTypeAssignedTargetingOptionController : ControllerBase
{
    private readonly IDisplayVideoPartnerTargetingTypeAssignedTargetingOptionService _partnerTargetingTypeAssignedTargetingOptionService;

    public DisplayVideoPartnerTargetingTypeAssignedTargetingOptionController(IDisplayVideoPartnerTargetingTypeAssignedTargetingOptionService partnerTargetingTypeAssignedTargetingOptionService)
    {
        _partnerTargetingTypeAssignedTargetingOptionService = partnerTargetingTypeAssignedTargetingOptionService;
    }

    /// <summary>Assigns a targeting option to a partner.</summary>
    [HttpPost("partners/{partnerId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions")]
    public async Task<IActionResult> CreateAssignedTargetingOptionAsync([FromRoute] string partnerId, [FromRoute] string targetingTypeId, [FromBody] CreateAssignedTargetingOptionRequest request)
    {
        request.PartnerId = partnerId;
        request.TargetingTypeId = targetingTypeId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _partnerTargetingTypeAssignedTargetingOptionService.CreateAssignedTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an assigned targeting option from a partner.</summary>
    [HttpDelete("partners/{partnerId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions/{assignedTargetingOptionId}")]
    public async Task<IActionResult> DeleteAssignedTargetingOptionAsync([FromQuery] string accountKey, [FromRoute] string partnerId, [FromRoute] string targetingTypeId, [FromRoute] string assignedTargetingOptionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteAssignedTargetingOptionRequest { AccountKey = accountKey, PartnerId = partnerId, TargetingTypeId = targetingTypeId, AssignedTargetingOptionId = assignedTargetingOptionId };
        var result = await _partnerTargetingTypeAssignedTargetingOptionService.DeleteAssignedTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a single targeting option assigned to a partner.</summary>
    [HttpGet("partners/{partnerId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions/{assignedTargetingOptionId}")]
    public async Task<IActionResult> GetAssignedTargetingOptionAsync([FromQuery] string accountKey, [FromRoute] string partnerId, [FromRoute] string targetingTypeId, [FromRoute] string assignedTargetingOptionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAssignedTargetingOptionRequest { AccountKey = accountKey, PartnerId = partnerId, TargetingTypeId = targetingTypeId, AssignedTargetingOptionId = assignedTargetingOptionId };
        var result = await _partnerTargetingTypeAssignedTargetingOptionService.GetAssignedTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists the targeting options assigned to a partner.</summary>
    [HttpGet("partners/{partnerId}/targetingTypes/{targetingTypeId}/assignedTargetingOptions")]
    public async Task<IActionResult> ListAssignedTargetingOptionsAsync([FromQuery] string accountKey, [FromRoute] string partnerId, [FromRoute] string targetingTypeId, [FromQuery] string? orderBy = null, [FromQuery] string? filter = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAssignedTargetingOptionsRequest { AccountKey = accountKey, PartnerId = partnerId, TargetingTypeId = targetingTypeId, OrderBy = orderBy, Filter = filter, PageSize = pageSize, PageToken = pageToken };
        var result = await _partnerTargetingTypeAssignedTargetingOptionService.ListAssignedTargetingOptionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
