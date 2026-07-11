using ApiToolKit.Application.DTOs.YouTube.Caption;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.YouTube;

[ApiController]
[Route("api/youtube/captions")]
[Produces("application/json")]
public class YouTubeCaptionController : ControllerBase
{
    private readonly IYouTubeCaptionService _captionService;

    public YouTubeCaptionController(IYouTubeCaptionService captionService)
    {
        _captionService = captionService;
    }

    private string GetAccessToken() => Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    /// <summary>List captions for a video</summary>
    [HttpGet]
    public async Task<IActionResult> ListCaptions([FromQuery] ListCaptionsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _captionService.ListCaptionsAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new caption track</summary>
    [HttpPost]
    public async Task<IActionResult> CreateCaption([FromBody] CreateCaptionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _captionService.CreateCaptionAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a caption track</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateCaption([FromBody] UpdateCaptionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _captionService.UpdateCaptionAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a caption track</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCaption([FromRoute] string id)
    {
        var request = new DeleteCaptionRequest { CaptionId = id };
        var result = await _captionService.DeleteCaptionAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
