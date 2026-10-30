using ApiToolKit.Application.DTOs.DisplayVideo.FloodlightGroupFloodlightActivity;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// floodlightGroups / floodlightActivities (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoFloodlightGroupFloodlightActivityController : ControllerBase
{
    private readonly IDisplayVideoFloodlightGroupFloodlightActivityService _floodlightGroupFloodlightActivityService;

    public DisplayVideoFloodlightGroupFloodlightActivityController(IDisplayVideoFloodlightGroupFloodlightActivityService floodlightGroupFloodlightActivityService)
    {
        _floodlightGroupFloodlightActivityService = floodlightGroupFloodlightActivityService;
    }

    /// <summary>Lists Floodlight activities in a Floodlight group.</summary>
    [HttpGet("floodlightGroups/{floodlightGroupId}/floodlightActivities")]
    public async Task<IActionResult> ListFloodlightActivitiesAsync([FromQuery] string accountKey, [FromRoute] string floodlightGroupId, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListFloodlightActivitiesRequest { AccountKey = accountKey, FloodlightGroupId = floodlightGroupId, PageSize = pageSize, OrderBy = orderBy, PartnerId = partnerId, PageToken = pageToken };
        var result = await _floodlightGroupFloodlightActivityService.ListFloodlightActivitiesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a Floodlight activity.</summary>
    [HttpGet("floodlightGroups/{floodlightGroupId}/floodlightActivities/{floodlightActivityId}")]
    public async Task<IActionResult> GetFloodlightActivityAsync([FromQuery] string accountKey, [FromRoute] string floodlightGroupId, [FromRoute] string floodlightActivityId, [FromQuery] string? partnerId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetFloodlightActivityRequest { AccountKey = accountKey, FloodlightGroupId = floodlightGroupId, FloodlightActivityId = floodlightActivityId, PartnerId = partnerId };
        var result = await _floodlightGroupFloodlightActivityService.GetFloodlightActivityAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
