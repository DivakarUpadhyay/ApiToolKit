using ApiToolKit.Application.DTOs.DisplayVideo.Channel;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// channels (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoChannelController : ControllerBase
{
    private readonly IDisplayVideoChannelService _channelService;

    public DisplayVideoChannelController(IDisplayVideoChannelService channelService)
    {
        _channelService = channelService;
    }

    /// <summary>Lists channels for a partner or advertiser.</summary>
    [HttpGet("advertisers/{advertiserId}/channels")]
    public async Task<IActionResult> ListChannelsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] int? pageSize = null, [FromQuery] string? filter = null, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null, [FromQuery] string? orderBy = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListChannelsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, PageSize = pageSize, Filter = filter, PartnerId = partnerId, PageToken = pageToken, OrderBy = orderBy };
        var result = await _channelService.ListChannelsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates a channel.</summary>
    [HttpPatch("advertisers/{advertiserId}/channels/{channelId}")]
    public async Task<IActionResult> UpdateChannelAsync([FromRoute] string advertiserId, [FromRoute] string channelId, [FromBody] UpdateChannelRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.ChannelId = channelId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _channelService.UpdateChannelAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new channel.</summary>
    [HttpPost("advertisers/{advertiserId}/channels")]
    public async Task<IActionResult> CreateChannelAsync([FromRoute] string advertiserId, [FromBody] CreateChannelRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _channelService.CreateChannelAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a channel for a partner or advertiser.</summary>
    [HttpGet("advertisers/{advertiserId}/channels/{channelId}")]
    public async Task<IActionResult> GetChannelAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string channelId, [FromQuery] string? partnerId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetChannelRequest { AccountKey = accountKey, AdvertiserId = advertiserId, ChannelId = channelId, PartnerId = partnerId };
        var result = await _channelService.GetChannelAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
