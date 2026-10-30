using ApiToolKit.Application.DTOs.DisplayVideo.TargetingTypeTargetingOption;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// targetingTypes / targetingOptions (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoTargetingTypeTargetingOptionController : ControllerBase
{
    private readonly IDisplayVideoTargetingTypeTargetingOptionService _targetingTypeTargetingOptionService;

    public DisplayVideoTargetingTypeTargetingOptionController(IDisplayVideoTargetingTypeTargetingOptionService targetingTypeTargetingOptionService)
    {
        _targetingTypeTargetingOptionService = targetingTypeTargetingOptionService;
    }

    /// <summary>Gets a single targeting option.</summary>
    [HttpGet("targetingTypes/{targetingTypeId}/targetingOptions/{targetingOptionId}")]
    public async Task<IActionResult> GetTargetingOptionAsync([FromQuery] string accountKey, [FromRoute] string targetingTypeId, [FromRoute] string targetingOptionId, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetTargetingOptionRequest { AccountKey = accountKey, TargetingTypeId = targetingTypeId, TargetingOptionId = targetingOptionId, AdvertiserId = advertiserId };
        var result = await _targetingTypeTargetingOptionService.GetTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Searches for targeting options of a given type based on the given search terms.</summary>
    [HttpPost("targetingTypes/{targetingTypeId}/targetingOptions/search")]
    public async Task<IActionResult> SearchTargetingOptionAsync([FromRoute] string targetingTypeId, [FromBody] SearchTargetingOptionRequest request)
    {
        request.TargetingTypeId = targetingTypeId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _targetingTypeTargetingOptionService.SearchTargetingOptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists targeting options of a given type.</summary>
    [HttpGet("targetingTypes/{targetingTypeId}/targetingOptions")]
    public async Task<IActionResult> ListTargetingOptionsAsync([FromQuery] string accountKey, [FromRoute] string targetingTypeId, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? advertiserId = null, [FromQuery] string? filter = null, [FromQuery] string? orderBy = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListTargetingOptionsRequest { AccountKey = accountKey, TargetingTypeId = targetingTypeId, PageToken = pageToken, PageSize = pageSize, AdvertiserId = advertiserId, Filter = filter, OrderBy = orderBy };
        var result = await _targetingTypeTargetingOptionService.ListTargetingOptionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
