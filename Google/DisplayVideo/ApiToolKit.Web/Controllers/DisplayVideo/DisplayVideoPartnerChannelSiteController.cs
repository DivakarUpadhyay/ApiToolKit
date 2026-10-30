using ApiToolKit.Application.DTOs.DisplayVideo.PartnerChannelSite;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// partners / channels / sites (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoPartnerChannelSiteController : ControllerBase
{
    private readonly IDisplayVideoPartnerChannelSiteService _partnerChannelSiteService;

    public DisplayVideoPartnerChannelSiteController(IDisplayVideoPartnerChannelSiteService partnerChannelSiteService)
    {
        _partnerChannelSiteService = partnerChannelSiteService;
    }

    /// <summary>Bulk edits sites under a single channel.</summary>
    [HttpPost("partners/{partnerId}/channels/{channelId}/sites/bulkEdit")]
    public async Task<IActionResult> BulkEditSiteAsync([FromRoute] string partnerId, [FromRoute] string channelId, [FromBody] BulkEditSiteRequest request)
    {
        request.PartnerId = partnerId;
        request.ChannelId = channelId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _partnerChannelSiteService.BulkEditSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a site in a channel.</summary>
    [HttpPost("partners/{partnerId}/channels/{channelId}/sites")]
    public async Task<IActionResult> CreateSiteAsync([FromRoute] string partnerId, [FromRoute] string channelId, [FromBody] CreateSiteRequest request)
    {
        request.PartnerId = partnerId;
        request.ChannelId = channelId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _partnerChannelSiteService.CreateSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Replaces all of the sites under a single channel.</summary>
    [HttpPost("partners/{partnerId}/channels/{channelId}/sites/replace")]
    public async Task<IActionResult> ReplaceSiteAsync([FromRoute] string partnerId, [FromRoute] string channelId, [FromBody] ReplaceSiteRequest request)
    {
        request.PartnerId = partnerId;
        request.ChannelId = channelId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _partnerChannelSiteService.ReplaceSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists sites in a channel.</summary>
    [HttpGet("partners/{partnerId}/channels/{channelId}/sites")]
    public async Task<IActionResult> ListSitesAsync([FromQuery] string accountKey, [FromRoute] string partnerId, [FromRoute] string channelId, [FromQuery] string? advertiserId = null, [FromQuery] string? filter = null, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListSitesRequest { AccountKey = accountKey, PartnerId = partnerId, ChannelId = channelId, AdvertiserId = advertiserId, Filter = filter, PageToken = pageToken, PageSize = pageSize, OrderBy = orderBy };
        var result = await _partnerChannelSiteService.ListSitesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a site from a channel.</summary>
    [HttpDelete("partners/{partnerId}/channels/{channelId}/sites/{siteId}")]
    public async Task<IActionResult> DeleteSiteAsync([FromQuery] string accountKey, [FromRoute] string partnerId, [FromRoute] string channelId, [FromRoute] string siteId, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteSiteRequest { AccountKey = accountKey, PartnerId = partnerId, ChannelId = channelId, SiteId = siteId, AdvertiserId = advertiserId };
        var result = await _partnerChannelSiteService.DeleteSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
