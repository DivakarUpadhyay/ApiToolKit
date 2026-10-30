using ApiToolKit.Application.DTOs.DisplayVideo.Media;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// media (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoMediaController : ControllerBase
{
    private readonly IDisplayVideoMediaService _mediaService;

    public DisplayVideoMediaController(IDisplayVideoMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    /// <summary>Downloads media.</summary>
    [HttpGet("download/{downloadId}")]
    public async Task<IActionResult> DownloadMediaAsync([FromQuery] string accountKey, [FromRoute] string downloadId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DownloadMediaRequest { AccountKey = accountKey, DownloadId = downloadId };
        var result = await _mediaService.DownloadMediaAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Download the file contents of: Downloads media.</summary>
    [HttpGet("download/{downloadId}/download")]
    public async Task<IActionResult> DownloadMediaContentAsync([FromQuery] string accountKey, [FromRoute] string downloadId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DownloadMediaContentRequest { AccountKey = accountKey, DownloadId = downloadId };
        var result = await _mediaService.DownloadMediaContentAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
