using ApiToolKit.Application.DTOs.Facebook.Insight;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/insight")]
[Produces("application/json")]
public class FacebookInsightController : ControllerBase
{
    private readonly IFacebookInsightService _insightService;

    public FacebookInsightController(IFacebookInsightService insightService)
    {
        _insightService = insightService;
    }

    /// <summary>Get page-level insights and metrics</summary>
    [HttpPost("page")]
    public async Task<IActionResult> GetPageInsightsAsync([FromBody] GetPageInsightsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _insightService.GetPageInsightsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get video-level insights and metrics</summary>
    [HttpPost("video")]
    public async Task<IActionResult> GetVideoInsightsAsync([FromBody] VideoInsightRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _insightService.GetVideoInsightsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get ad-level insights and performance data</summary>
    [HttpPost("ad")]
    public async Task<IActionResult> GetAdInsightsAsync([FromBody] GetAdInsightsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _insightService.GetAdInsightsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get campaign-level insights and performance data</summary>
    [HttpGet("campaign/{campaignId}")]
    public async Task<IActionResult> GetCampaignInsightsAsync(string campaignId, [FromQuery] string accessToken, [FromQuery] string? datePreset = null, [FromQuery] int limit = 25)
    {
        var result = await _insightService.GetCampaignInsightsAsync(campaignId, accessToken, datePreset, limit);
        return Ok(result);
    }

    /// <summary>Get ad set-level insights and performance data</summary>
    [HttpGet("adset/{adSetId}")]
    public async Task<IActionResult> GetAdSetInsightsAsync(string adSetId, [FromQuery] string accessToken, [FromQuery] string? datePreset = null, [FromQuery] int limit = 25)
    {
        var result = await _insightService.GetAdSetInsightsAsync(adSetId, accessToken, datePreset, limit);
        return Ok(result);
    }

    /// <summary>Get account-level insights and performance data</summary>
    [HttpGet("account/{accountId}")]
    public async Task<IActionResult> GetAccountInsightsAsync(string accountId, [FromQuery] string accessToken, [FromQuery] string? datePreset = null, [FromQuery] int limit = 25)
    {
        var result = await _insightService.GetAccountInsightsAsync(accountId, accessToken, datePreset, limit);
        return Ok(result);
    }
}
