using ApiToolKit.Application.DTOs.Analytics.DataApi;
using ApiToolKit.Application.Interfaces.Services.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Analytics;

/// <summary>
/// Remaining Google Analytics Data API v1beta methods: batch pivot reports, compatibility check and audience exports.
/// All endpoints require a valid Bearer access token in the Authorization header.
/// </summary>
[ApiController]
[Route("api/analytics")]
public class AnalyticsDataApiController : ControllerBase
{
    private readonly IAnalyticsDataApiService _dataApiService;

    public AnalyticsDataApiController(IAnalyticsDataApiService dataApiService)
    {
        _dataApiService = dataApiService;
    }

    private string GetAccessToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        return authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
            ? authHeader["Bearer ".Length..]
            : authHeader;
    }

    /// <summary>
    /// Run several pivot reports in one request (body: { "requests": [ RunPivotReportRequest, ... ] }).
    /// </summary>
    [HttpPost("reports/pivot/batch")]
    public async Task<IActionResult> BatchRunPivotReportsAsync([FromBody] PropertyBodyRequest request)
    {
        var result = await _dataApiService.BatchRunPivotReportsAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Check which dimensions and metrics can be combined in one Core report.
    /// </summary>
    [HttpPost("reports/compatibility")]
    public async Task<IActionResult> CheckCompatibilityAsync([FromBody] PropertyBodyRequest request)
    {
        var result = await _dataApiService.CheckCompatibilityAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// List audience exports of a property.
    /// </summary>
    [HttpGet("audience-exports/{propertyId}")]
    public async Task<IActionResult> ListAudienceExportsAsync([FromRoute] string propertyId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        var result = await _dataApiService.ListAudienceExportsAsync(propertyId, pageSize, pageToken, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Get one audience export (state, row count, creation time).
    /// </summary>
    [HttpGet("audience-exports/{propertyId}/{audienceExportId}")]
    public async Task<IActionResult> GetAudienceExportAsync([FromRoute] string propertyId, [FromRoute] string audienceExportId)
    {
        var result = await _dataApiService.GetAudienceExportAsync(propertyId, audienceExportId, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Create an audience export (body: { "audience": "properties/123/audiences/456", "dimensions": [ { "dimensionName": "deviceId" } ] }).
    /// </summary>
    [HttpPost("audience-exports")]
    public async Task<IActionResult> CreateAudienceExportAsync([FromBody] PropertyBodyRequest request)
    {
        var result = await _dataApiService.CreateAudienceExportAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Read the users in a finished audience export (body: { "offset": 0, "limit": 10000 }).
    /// </summary>
    [HttpPost("audience-exports/query")]
    public async Task<IActionResult> QueryAudienceExportAsync([FromBody] PropertyResourceBodyRequest request)
    {
        var result = await _dataApiService.QueryAudienceExportAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
