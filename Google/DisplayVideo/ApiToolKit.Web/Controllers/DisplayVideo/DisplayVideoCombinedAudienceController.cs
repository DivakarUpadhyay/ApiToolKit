using ApiToolKit.Application.DTOs.DisplayVideo.CombinedAudience;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// combinedAudiences (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoCombinedAudienceController : ControllerBase
{
    private readonly IDisplayVideoCombinedAudienceService _combinedAudienceService;

    public DisplayVideoCombinedAudienceController(IDisplayVideoCombinedAudienceService combinedAudienceService)
    {
        _combinedAudienceService = combinedAudienceService;
    }

    /// <summary>Lists combined audiences.</summary>
    [HttpGet("combinedAudiences")]
    public async Task<IActionResult> ListCombinedAudiencesAsync([FromQuery] string accountKey, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null, [FromQuery] string? filter = null, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListCombinedAudiencesRequest { AccountKey = accountKey, PageSize = pageSize, OrderBy = orderBy, Filter = filter, PartnerId = partnerId, PageToken = pageToken, AdvertiserId = advertiserId };
        var result = await _combinedAudienceService.ListCombinedAudiencesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a combined audience.</summary>
    [HttpGet("combinedAudiences/{combinedAudienceId}")]
    public async Task<IActionResult> GetCombinedAudienceAsync([FromQuery] string accountKey, [FromRoute] string combinedAudienceId, [FromQuery] string? advertiserId = null, [FromQuery] string? partnerId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetCombinedAudienceRequest { AccountKey = accountKey, CombinedAudienceId = combinedAudienceId, AdvertiserId = advertiserId, PartnerId = partnerId };
        var result = await _combinedAudienceService.GetCombinedAudienceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
