using ApiToolKit.Application.DTOs.Facebook.Video;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/video")]
[Produces("application/json")]
public class FacebookVideoController : ControllerBase
{
    private readonly IFacebookVideoService _videoService;

    public FacebookVideoController(IFacebookVideoService videoService)
    {
        _videoService = videoService;
    }

    /// <summary>Upload a video to a page</summary>
    [HttpPost("upload")]
    public async Task<IActionResult> UploadVideoAsync([FromBody] UploadVideoRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _videoService.UploadVideoAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get video details by ID</summary>
    [HttpGet("{videoId}")]
    public async Task<IActionResult> GetVideoAsync(string videoId, [FromQuery] string accessToken)
    {
        var result = await _videoService.GetVideoAsync(videoId, accessToken);
        return Ok(result);
    }

    /// <summary>Get videos for an object (page or user)</summary>
    [HttpPost("list")]
    public async Task<IActionResult> GetVideosAsync([FromBody] GetVideosRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _videoService.GetVideosAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Update video metadata</summary>
    [HttpPut("{videoId}")]
    public async Task<IActionResult> UpdateVideoAsync(string videoId, [FromQuery] string? title, [FromQuery] string? description, [FromQuery] string accessToken)
    {
        var result = await _videoService.UpdateVideoAsync(videoId, title, description, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a video</summary>
    [HttpDelete("{videoId}")]
    public async Task<IActionResult> DeleteVideoAsync(string videoId, [FromQuery] string accessToken)
    {
        var result = await _videoService.DeleteVideoAsync(videoId, accessToken);
        return Ok(result);
    }

    /// <summary>Get processing status of a video</summary>
    [HttpGet("{videoId}/status")]
    public async Task<IActionResult> GetVideoStatusAsync(string videoId, [FromQuery] string accessToken)
    {
        var result = await _videoService.GetVideoStatusAsync(videoId, accessToken);
        return Ok(result);
    }

    /// <summary>Get thumbnail options for a video</summary>
    [HttpGet("{videoId}/thumbnails")]
    public async Task<IActionResult> GetVideoThumbnailsAsync(string videoId, [FromQuery] string accessToken)
    {
        var result = await _videoService.GetVideoThumbnailsAsync(videoId, accessToken);
        return Ok(result);
    }
}
