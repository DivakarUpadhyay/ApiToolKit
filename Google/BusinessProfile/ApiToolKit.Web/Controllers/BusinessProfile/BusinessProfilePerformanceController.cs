using ApiToolKit.Application.DTOs.BusinessProfile.Performance;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BusinessProfile;

/// <summary>
/// Profile performance (Performance API): views, calls, directions, website clicks, search keywords.
/// </summary>
[ApiController]
[Route("api/businessprofile/performance")]
[Produces("application/json")]
public class BusinessProfilePerformanceController : ControllerBase
{
    private readonly IBusinessProfilePerformanceService _performanceService;

    public BusinessProfilePerformanceController(IBusinessProfilePerformanceService performanceService)
    {
        _performanceService = performanceService;
    }

    /// <summary>Daily metrics such as BUSINESS_IMPRESSIONS_DESKTOP_SEARCH, CALL_CLICKS, WEBSITE_CLICKS, BUSINESS_DIRECTION_REQUESTS.</summary>
    [HttpGet("daily-metrics")]
    public async Task<IActionResult> GetDailyMetricsAsync([FromQuery] string accountKey, [FromQuery] string locationId, [FromQuery] List<string> dailyMetrics, [FromQuery] string startDate, [FromQuery] string endDate)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        if (dailyMetrics == null || dailyMetrics.Count == 0) return BadRequest("dailyMetrics is required.");
        if (string.IsNullOrEmpty(startDate)) return BadRequest("startDate is required.");
        if (string.IsNullOrEmpty(endDate)) return BadRequest("endDate is required.");
        var request = new GetDailyMetricsRequest { AccountKey = accountKey, LocationId = locationId, DailyMetrics = dailyMetrics, StartDate = startDate, EndDate = endDate };
        var result = await _performanceService.GetDailyMetricsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Monthly search keywords that showed the business in Search and Maps.</summary>
    [HttpGet("search-keywords")]
    public async Task<IActionResult> GetSearchKeywordImpressionsAsync([FromQuery] string accountKey, [FromQuery] string locationId, [FromQuery] string startMonth, [FromQuery] string endMonth, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        if (string.IsNullOrEmpty(startMonth)) return BadRequest("startMonth is required.");
        if (string.IsNullOrEmpty(endMonth)) return BadRequest("endMonth is required.");
        var request = new GetSearchKeywordImpressionsRequest { AccountKey = accountKey, LocationId = locationId, StartMonth = startMonth, EndMonth = endMonth, PageSize = pageSize, PageToken = pageToken };
        var result = await _performanceService.GetSearchKeywordImpressionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Single daily metric time series for a location.</summary>
    [HttpGet("daily-metric")]
    public async Task<IActionResult> GetDailyMetricTimeSeriesAsync([FromQuery] string accountKey, [FromQuery] string locationId, [FromQuery] string dailyMetric, [FromQuery] string startDate, [FromQuery] string endDate)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        if (string.IsNullOrEmpty(dailyMetric)) return BadRequest("dailyMetric is required.");
        if (string.IsNullOrEmpty(startDate)) return BadRequest("startDate is required.");
        if (string.IsNullOrEmpty(endDate)) return BadRequest("endDate is required.");
        var request = new GetDailyMetricTimeSeriesRequest { AccountKey = accountKey, LocationId = locationId, DailyMetric = dailyMetric, StartDate = startDate, EndDate = endDate };
        var result = await _performanceService.GetDailyMetricTimeSeriesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
