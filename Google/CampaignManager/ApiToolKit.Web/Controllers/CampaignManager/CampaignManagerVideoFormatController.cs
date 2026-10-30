using ApiToolKit.Application.DTOs.CampaignManager.VideoFormat;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// videoFormats (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerVideoFormatController : ControllerBase
{
    private readonly ICampaignManagerVideoFormatService _videoFormatService;

    public CampaignManagerVideoFormatController(ICampaignManagerVideoFormatService videoFormatService)
    {
        _videoFormatService = videoFormatService;
    }

    /// <summary>Lists available video formats.</summary>
    [HttpGet("userprofiles/{userprofileId}/videoFormats")]
    public async Task<IActionResult> ListVideoFormatsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListVideoFormatsRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _videoFormatService.ListVideoFormatsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one video format by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/videoFormats/{videoFormatId}")]
    public async Task<IActionResult> GetVideoFormatAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string videoFormatId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetVideoFormatRequest { AccountKey = accountKey, UserprofileId = userprofileId, VideoFormatId = videoFormatId };
        var result = await _videoFormatService.GetVideoFormatAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
