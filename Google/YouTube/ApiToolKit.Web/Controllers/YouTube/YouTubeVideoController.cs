using ApiToolKit.Application.DTOs.YouTube.Video;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.YouTube;

[ApiController]
[Route("api/youtube/videos")]
[Produces("application/json")]
public class YouTubeVideoController : ControllerBase
{
    private readonly IYouTubeVideoService _videoService;

    public YouTubeVideoController(IYouTubeVideoService videoService)
    {
        _videoService = videoService;
    }

    private string GetAccessToken() => Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    /// <summary>List videos for a channel</summary>
    [HttpGet("list")]
    public async Task<IActionResult> ListVideos([FromQuery] ListVideosRequest request)
    {
        var result = await _videoService.ListVideosAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get details of a specific video</summary>
    [HttpGet("{videoId}")]
    public async Task<IActionResult> GetVideo([FromRoute] string videoId)
    {
        var request = new GetVideoRequest { VideoId = videoId };
        var result = await _videoService.GetVideoAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update video metadata</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateVideo([FromBody] UpdateVideoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _videoService.UpdateVideoAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a video</summary>
    [HttpDelete("{videoId}")]
    public async Task<IActionResult> DeleteVideo([FromRoute] string videoId)
    {
        var request = new DeleteVideoRequest { VideoId = videoId };
        var result = await _videoService.DeleteVideoAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Rate a video (like, dislike, none)</summary>
    [HttpPost("rate")]
    public async Task<IActionResult> RateVideo([FromBody] RateVideoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _videoService.RateVideoAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Upload a single video with metadata</summary>
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadVideo(
        [FromForm] UploadVideoMetadata metadata,
        IFormFile file)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (file == null || file.Length == 0)
            return BadRequest(new { success = false, message = "No video file provided." });

        using var stream = file.OpenReadStream();
        var mimeType = file.ContentType.StartsWith("video/") ? file.ContentType : "video/mp4";
        var result = await _videoService.UploadVideoAsync(metadata, stream, file.FileName, mimeType, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Upload multiple videos in a single request</summary>
    [HttpPost("bulk-upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> BulkUploadVideos(
        [FromForm] string titlesJson,
        [FromForm] string? descriptionsJson,
        [FromForm] string? privacyStatus,
        IFormFileCollection files)
    {
        if (files == null || files.Count == 0)
            return BadRequest(new { success = false, message = "No video files provided." });

        var titles = System.Text.Json.JsonSerializer.Deserialize<List<string>>(titlesJson) ?? new();
        var descriptions = descriptionsJson != null
            ? System.Text.Json.JsonSerializer.Deserialize<List<string>>(descriptionsJson)
            : null;

        var uploads = new List<(UploadVideoMetadata, Stream, string, string)>();
        for (var i = 0; i < files.Count; i++)
        {
            var file = files[i];
            var meta = new UploadVideoMetadata
            {
                Title = i < titles.Count ? titles[i] : Path.GetFileNameWithoutExtension(file.FileName),
                Description = descriptions != null && i < descriptions.Count ? descriptions[i] : null,
                PrivacyStatus = privacyStatus ?? "public"
            };
            var mimeType = file.ContentType.StartsWith("video/") ? file.ContentType : "video/mp4";
            uploads.Add((meta, file.OpenReadStream(), file.FileName, mimeType));
        }

        var result = await _videoService.BulkUploadVideosAsync(uploads, GetAccessToken());

        foreach (var (_, stream, _, _) in uploads)
            await stream.DisposeAsync();

        return result.Success ? Ok(result) : BadRequest(result);
    }
}
