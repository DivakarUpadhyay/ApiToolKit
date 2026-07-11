using ApiToolKit.Application.DTOs.YouTube.Analytics;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.YouTube;

[ApiController]
[Route("api/youtube/analytics")]
[Produces("application/json")]
public class YouTubeAnalyticsController : ControllerBase
{
    private readonly IYouTubeAnalyticsService _analyticsService;

    public YouTubeAnalyticsController(IYouTubeAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    private string GetAccessToken() => Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    /// <summary>Run a YouTube Analytics report</summary>
    [HttpPost("report")]
    public async Task<IActionResult> RunReport([FromBody] RunAnalyticsReportRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _analyticsService.RunReportAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
