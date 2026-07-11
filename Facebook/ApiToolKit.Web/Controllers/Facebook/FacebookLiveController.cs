using ApiToolKit.Application.DTOs.Facebook.Live;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;
namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/live")]
[Produces("application/json")]
public class FacebookLiveController : ControllerBase
{
    private readonly IFacebookLiveService _service;
    public FacebookLiveController(IFacebookLiveService service) => _service = service;

    /// <summary>Create a new live video broadcast for a Facebook Page.</summary>
    [HttpPost("{pageId}")]
    public async Task<IActionResult> CreateLiveVideo(string pageId, [FromBody] CreateLiveVideoRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.CreateLiveVideoAsync(pageId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Get all live videos for a Facebook Page.</summary>
    [HttpGet("{pageId}/videos")]
    public async Task<IActionResult> GetLiveVideos(string pageId, [FromQuery] string accessToken)
    {
        var result = await _service.GetLiveVideosAsync(pageId, accessToken);
        return Ok(result);
    }

    /// <summary>Get details of a specific live video by ID.</summary>
    [HttpGet("video/{liveVideoId}")]
    public async Task<IActionResult> GetLiveVideo(string liveVideoId, [FromQuery] string accessToken)
    {
        var result = await _service.GetLiveVideoAsync(liveVideoId, accessToken);
        return Ok(result);
    }

    /// <summary>Update the title, description, or status of a live video.</summary>
    [HttpPut("video/{liveVideoId}")]
    public async Task<IActionResult> UpdateLiveVideo(string liveVideoId, [FromBody] UpdateLiveVideoRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.UpdateLiveVideoAsync(liveVideoId, request, accessToken);
        return Ok(result);
    }

    /// <summary>End an active live video broadcast.</summary>
    [HttpPost("video/{liveVideoId}/end")]
    public async Task<IActionResult> EndLiveVideo(string liveVideoId, [FromQuery] string accessToken)
    {
        var result = await _service.EndLiveVideoAsync(liveVideoId, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a live video by ID.</summary>
    [HttpDelete("video/{liveVideoId}")]
    public async Task<IActionResult> DeleteLiveVideo(string liveVideoId, [FromQuery] string accessToken)
    {
        var result = await _service.DeleteLiveVideoAsync(liveVideoId, accessToken);
        return Ok(result);
    }

    /// <summary>Get stream errors that occurred during a live video broadcast.</summary>
    [HttpGet("video/{liveVideoId}/errors")]
    public async Task<IActionResult> GetLiveVideoErrors(string liveVideoId, [FromQuery] string accessToken)
    {
        var result = await _service.GetLiveVideoErrorsAsync(liveVideoId, accessToken);
        return Ok(result);
    }

    /// <summary>Get users blocked from a live video broadcast.</summary>
    [HttpGet("video/{liveVideoId}/blocked-users")]
    public async Task<IActionResult> GetBlockedUsers(string liveVideoId, [FromQuery] string accessToken)
    {
        var result = await _service.GetBlockedUsersAsync(liveVideoId, accessToken);
        return Ok(result);
    }
}
