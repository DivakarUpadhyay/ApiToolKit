using ApiToolKit.Application.DTOs.DisplayVideo.PartnerChannel;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// partners / channels (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoPartnerChannelController : ControllerBase
{
    private readonly IDisplayVideoPartnerChannelService _partnerChannelService;

    public DisplayVideoPartnerChannelController(IDisplayVideoPartnerChannelService partnerChannelService)
    {
        _partnerChannelService = partnerChannelService;
    }

    /// <summary>Lists channels for a partner or advertiser.</summary>
    [HttpGet("partners/{partnerId}/channels")]
    public async Task<IActionResult> ListChannelsAsync([FromQuery] string accountKey, [FromRoute] string partnerId, [FromQuery] string? advertiserId = null, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? filter = null, [FromQuery] string? orderBy = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListChannelsRequest { AccountKey = accountKey, PartnerId = partnerId, AdvertiserId = advertiserId, PageToken = pageToken, PageSize = pageSize, Filter = filter, OrderBy = orderBy };
        var result = await _partnerChannelService.ListChannelsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a channel for a partner or advertiser.</summary>
    [HttpGet("partners/{partnerId}/channels/{channelId}")]
    public async Task<IActionResult> GetChannelAsync([FromQuery] string accountKey, [FromRoute] string partnerId, [FromRoute] string channelId, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetChannelRequest { AccountKey = accountKey, PartnerId = partnerId, ChannelId = channelId, AdvertiserId = advertiserId };
        var result = await _partnerChannelService.GetChannelAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new channel.</summary>
    [HttpPost("partners/{partnerId}/channels")]
    public async Task<IActionResult> CreateChannelAsync([FromRoute] string partnerId, [FromBody] CreateChannelRequest request)
    {
        request.PartnerId = partnerId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _partnerChannelService.CreateChannelAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates a channel.</summary>
    [HttpPatch("partners/{partnerId}/channels/{channelId}")]
    public async Task<IActionResult> UpdateChannelAsync([FromRoute] string partnerId, [FromRoute] string channelId, [FromBody] UpdateChannelRequest request)
    {
        request.PartnerId = partnerId;
        request.ChannelId = channelId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _partnerChannelService.UpdateChannelAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
