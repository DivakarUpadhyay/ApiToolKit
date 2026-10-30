using ApiToolKit.Application.DTOs.DisplayVideo.ChannelSite;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// channels / sites (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoChannelSiteController : ControllerBase
{
    private readonly IDisplayVideoChannelSiteService _channelSiteService;

    public DisplayVideoChannelSiteController(IDisplayVideoChannelSiteService channelSiteService)
    {
        _channelSiteService = channelSiteService;
    }

    /// <summary>Creates a site in a channel.</summary>
    [HttpPost("advertisers/{advertiserId}/channels/{channelId}/sites")]
    public async Task<IActionResult> CreateSiteAsync([FromRoute] string advertiserId, [FromRoute] string channelId, [FromBody] CreateSiteRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.ChannelId = channelId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _channelSiteService.CreateSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists sites in a channel.</summary>
    [HttpGet("advertisers/{advertiserId}/channels/{channelId}/sites")]
    public async Task<IActionResult> ListSitesAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string channelId, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? filter = null, [FromQuery] string? orderBy = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListSitesRequest { AccountKey = accountKey, AdvertiserId = advertiserId, ChannelId = channelId, PartnerId = partnerId, PageToken = pageToken, PageSize = pageSize, Filter = filter, OrderBy = orderBy };
        var result = await _channelSiteService.ListSitesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Replaces all of the sites under a single channel.</summary>
    [HttpPost("advertisers/{advertiserId}/channels/{channelId}/sites/replace")]
    public async Task<IActionResult> ReplaceSiteAsync([FromRoute] string advertiserId, [FromRoute] string channelId, [FromBody] ReplaceSiteRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.ChannelId = channelId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _channelSiteService.ReplaceSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Bulk edits sites under a single channel.</summary>
    [HttpPost("advertisers/{advertiserId}/channels/{channelId}/sites/bulkEdit")]
    public async Task<IActionResult> BulkEditSiteAsync([FromRoute] string advertiserId, [FromRoute] string channelId, [FromBody] BulkEditSiteRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.ChannelId = channelId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _channelSiteService.BulkEditSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a site from a channel.</summary>
    [HttpDelete("advertisers/{advertiserId}/channels/{channelId}/sites/{siteId}")]
    public async Task<IActionResult> DeleteSiteAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string channelId, [FromRoute] string siteId, [FromQuery] string? partnerId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteSiteRequest { AccountKey = accountKey, AdvertiserId = advertiserId, ChannelId = channelId, SiteId = siteId, PartnerId = partnerId };
        var result = await _channelSiteService.DeleteSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
