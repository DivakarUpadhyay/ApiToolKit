using ApiToolKit.Application.DTOs.DisplayVideo.GoogleAudience;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// googleAudiences (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoGoogleAudienceController : ControllerBase
{
    private readonly IDisplayVideoGoogleAudienceService _googleAudienceService;

    public DisplayVideoGoogleAudienceController(IDisplayVideoGoogleAudienceService googleAudienceService)
    {
        _googleAudienceService = googleAudienceService;
    }

    /// <summary>Lists Google audiences.</summary>
    [HttpGet("googleAudiences")]
    public async Task<IActionResult> ListGoogleAudiencesAsync([FromQuery] string accountKey, [FromQuery] string? orderBy = null, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? filter = null, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListGoogleAudiencesRequest { AccountKey = accountKey, OrderBy = orderBy, PartnerId = partnerId, PageToken = pageToken, PageSize = pageSize, Filter = filter, AdvertiserId = advertiserId };
        var result = await _googleAudienceService.ListGoogleAudiencesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a Google audience.</summary>
    [HttpGet("googleAudiences/{googleAudienceId}")]
    public async Task<IActionResult> GetGoogleAudienceAsync([FromQuery] string accountKey, [FromRoute] string googleAudienceId, [FromQuery] string? advertiserId = null, [FromQuery] string? partnerId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetGoogleAudienceRequest { AccountKey = accountKey, GoogleAudienceId = googleAudienceId, AdvertiserId = advertiserId, PartnerId = partnerId };
        var result = await _googleAudienceService.GetGoogleAudienceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
