using ApiToolKit.Application.DTOs.SearchConsole.SearchAnalytics;
using ApiToolKit.Application.Interfaces.Services.SearchConsole;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.SearchConsole;

/// <summary>
/// Organic search performance: clicks, impressions, CTR and position.
/// </summary>
[ApiController]
[Route("api/searchconsole/search-analytics")]
[Produces("application/json")]
public class SearchConsoleSearchAnalyticsController : ControllerBase
{
    private readonly ISearchConsoleSearchAnalyticsService _searchAnalyticsService;

    public SearchConsoleSearchAnalyticsController(ISearchConsoleSearchAnalyticsService searchAnalyticsService)
    {
        _searchAnalyticsService = searchAnalyticsService;
    }

    /// <summary>Query clicks, impressions, CTR and average position grouped by any dimensions.</summary>
    [HttpPost("query")]
    public async Task<IActionResult> QuerySearchAnalyticsAsync([FromBody] QuerySearchAnalyticsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _searchAnalyticsService.QuerySearchAnalyticsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Keyword rankings: top search queries with clicks, impressions, CTR and average position.</summary>
    [HttpPost("keyword-rankings")]
    public async Task<IActionResult> GetKeywordRankingsAsync([FromBody] GetKeywordRankingsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _searchAnalyticsService.GetKeywordRankingsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Top landing pages from organic search with clicks, impressions, CTR and position.</summary>
    [HttpPost("top-pages")]
    public async Task<IActionResult> GetTopPagesAsync([FromBody] GetTopPagesRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _searchAnalyticsService.GetTopPagesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
