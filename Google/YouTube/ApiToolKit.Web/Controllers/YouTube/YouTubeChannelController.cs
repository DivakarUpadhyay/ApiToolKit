using ApiToolKit.Application.DTOs.YouTube.Channel;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.YouTube;

[ApiController]
[Route("api/youtube/channels")]
[Produces("application/json")]
public class YouTubeChannelController : ControllerBase
{
    private readonly IYouTubeChannelService _channelService;

    public YouTubeChannelController(IYouTubeChannelService channelService)
    {
        _channelService = channelService;
    }

    private string GetAccessToken() => Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    /// <summary>Get the authenticated user's channels</summary>
    [HttpGet("me")]
    public async Task<IActionResult> GetMyChannels()
    {
        var result = await _channelService.GetMyChannelsAsync(GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a channel by ID or handle</summary>
    [HttpGet("{channelId}")]
    public async Task<IActionResult> GetChannel([FromRoute] string channelId)
    {
        var request = new GetChannelRequest { ChannelId = channelId };
        var result = await _channelService.GetChannelAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update channel branding settings</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateChannel([FromBody] UpdateChannelRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _channelService.UpdateChannelAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
