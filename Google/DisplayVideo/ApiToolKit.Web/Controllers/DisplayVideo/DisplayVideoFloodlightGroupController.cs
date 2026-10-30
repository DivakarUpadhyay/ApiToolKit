using ApiToolKit.Application.DTOs.DisplayVideo.FloodlightGroup;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// floodlightGroups (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoFloodlightGroupController : ControllerBase
{
    private readonly IDisplayVideoFloodlightGroupService _floodlightGroupService;

    public DisplayVideoFloodlightGroupController(IDisplayVideoFloodlightGroupService floodlightGroupService)
    {
        _floodlightGroupService = floodlightGroupService;
    }

    /// <summary>Updates an existing Floodlight group.</summary>
    [HttpPatch("floodlightGroups/{floodlightGroupId}")]
    public async Task<IActionResult> UpdateFloodlightGroupAsync([FromRoute] string floodlightGroupId, [FromBody] UpdateFloodlightGroupRequest request)
    {
        request.FloodlightGroupId = floodlightGroupId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _floodlightGroupService.UpdateFloodlightGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a Floodlight group.</summary>
    [HttpGet("floodlightGroups/{floodlightGroupId}")]
    public async Task<IActionResult> GetFloodlightGroupAsync([FromQuery] string accountKey, [FromRoute] string floodlightGroupId, [FromQuery] string? partnerId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetFloodlightGroupRequest { AccountKey = accountKey, FloodlightGroupId = floodlightGroupId, PartnerId = partnerId };
        var result = await _floodlightGroupService.GetFloodlightGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
