using ApiToolKit.Application.DTOs.Analytics.Realtime;
using ApiToolKit.Application.Interfaces.Services.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Analytics;

/// <summary>
/// Realtime report endpoints for Google Analytics (GA4).
/// </summary>
[ApiController]
[Route("api/analytics/realtime")]
public class AnalyticsRealtimeController : ControllerBase
{
    private readonly IAnalyticsRealtimeService _realtimeService;

    public AnalyticsRealtimeController(IAnalyticsRealtimeService realtimeService)
    {
        _realtimeService = realtimeService;
    }

    private string GetAccessToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        return authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
            ? authHeader["Bearer ".Length..]
            : authHeader;
    }

    /// <summary>
    /// Run a realtime report showing currently active users in GA4.
    /// </summary>
    [HttpPost("report")]
    public async Task<IActionResult> RunRealtimeReportAsync([FromBody] RunRealtimeReportRequest request)
    {
        var result = await _realtimeService.RunRealtimeReportAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
