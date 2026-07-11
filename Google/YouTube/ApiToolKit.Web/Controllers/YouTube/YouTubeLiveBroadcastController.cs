using ApiToolKit.Application.DTOs.YouTube.LiveBroadcast;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.YouTube;

[ApiController]
[Route("api/youtube/live-broadcasts")]
[Produces("application/json")]
public class YouTubeLiveBroadcastController : ControllerBase
{
    private readonly IYouTubeLiveBroadcastService _broadcastService;

    public YouTubeLiveBroadcastController(IYouTubeLiveBroadcastService broadcastService)
    {
        _broadcastService = broadcastService;
    }

    private string GetAccessToken() => Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    /// <summary>List live broadcasts</summary>
    [HttpGet]
    public async Task<IActionResult> ListLiveBroadcasts([FromQuery] ListLiveBroadcastsRequest request)
    {
        var result = await _broadcastService.ListLiveBroadcastsAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new live broadcast</summary>
    [HttpPost]
    public async Task<IActionResult> CreateLiveBroadcast([FromBody] CreateLiveBroadcastRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _broadcastService.CreateLiveBroadcastAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing live broadcast</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateLiveBroadcast([FromBody] UpdateLiveBroadcastRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _broadcastService.UpdateLiveBroadcastAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Transition a broadcast to a new status (testing, live, complete)</summary>
    [HttpPost("{id}/transition")]
    public async Task<IActionResult> TransitionBroadcast([FromRoute] string id, [FromBody] TransitionBroadcastRequest request)
    {
        request.BroadcastId = id;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _broadcastService.TransitionBroadcastAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Bind a broadcast to a live stream</summary>
    [HttpPost("{id}/bind")]
    public async Task<IActionResult> BindBroadcast([FromRoute] string id, [FromBody] BindBroadcastRequest request)
    {
        request.BroadcastId = id;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _broadcastService.BindBroadcastAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a live broadcast</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLiveBroadcast([FromRoute] string id)
    {
        var result = await _broadcastService.DeleteLiveBroadcastAsync(id, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
