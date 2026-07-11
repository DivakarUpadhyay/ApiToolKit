using ApiToolKit.Application.DTOs.YouTube.LiveStream;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.YouTube;

[ApiController]
[Route("api/youtube/live-streams")]
[Produces("application/json")]
public class YouTubeLiveStreamController : ControllerBase
{
    private readonly IYouTubeLiveStreamService _liveStreamService;

    public YouTubeLiveStreamController(IYouTubeLiveStreamService liveStreamService)
    {
        _liveStreamService = liveStreamService;
    }

    private string GetAccessToken() => Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    /// <summary>List live streams</summary>
    [HttpGet]
    public async Task<IActionResult> ListLiveStreams([FromQuery] ListLiveStreamsRequest request)
    {
        var result = await _liveStreamService.ListLiveStreamsAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new live stream</summary>
    [HttpPost]
    public async Task<IActionResult> CreateLiveStream([FromBody] CreateLiveStreamRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _liveStreamService.CreateLiveStreamAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing live stream</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateLiveStream([FromBody] UpdateLiveStreamRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _liveStreamService.UpdateLiveStreamAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a live stream</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLiveStream([FromRoute] string id)
    {
        var result = await _liveStreamService.DeleteLiveStreamAsync(id, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
