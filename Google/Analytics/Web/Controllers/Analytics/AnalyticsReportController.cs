using ApiToolKit.Application.DTOs.Analytics.FunnelReport;
using ApiToolKit.Application.DTOs.Analytics.PivotReport;
using ApiToolKit.Application.DTOs.Analytics.Reports;
using ApiToolKit.Application.Interfaces.Services.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Analytics;

/// <summary>
/// Report endpoints for Google Analytics Data API (GA4).
/// All endpoints require a valid Bearer access token in the Authorization header.
/// </summary>
[ApiController]
[Route("api/analytics/reports")]
public class AnalyticsReportController : ControllerBase
{
    private readonly IAnalyticsReportService _reportService;

    public AnalyticsReportController(IAnalyticsReportService reportService)
    {
        _reportService = reportService;
    }

    private string GetAccessToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        return authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
            ? authHeader["Bearer ".Length..]
            : authHeader;
    }

    /// <summary>
    /// Run a standard GA4 report for dimensions and metrics over specified date ranges.
    /// </summary>
    [HttpPost("run")]
    public async Task<IActionResult> RunReportAsync([FromBody] RunReportRequest request)
    {
        var result = await _reportService.RunReportAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Run multiple GA4 reports in a single batch request.
    /// </summary>
    [HttpPost("batch")]
    public async Task<IActionResult> BatchRunReportsAsync([FromBody] BatchRunReportsRequest request)
    {
        var result = await _reportService.BatchRunReportsAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Run a pivot report for multi-dimensional analysis in GA4.
    /// </summary>
    [HttpPost("pivot")]
    public async Task<IActionResult> RunPivotReportAsync([FromBody] RunPivotReportRequest request)
    {
        var result = await _reportService.RunPivotReportAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Run a funnel report to analyze user progression through conversion steps in GA4.
    /// </summary>
    [HttpPost("funnel")]
    public async Task<IActionResult> RunFunnelReportAsync([FromBody] RunFunnelReportRequest request)
    {
        var result = await _reportService.RunFunnelReportAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
