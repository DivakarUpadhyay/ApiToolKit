using ApiToolKit.Application.DTOs.Analytics.DataApi;
using ApiToolKit.Application.Interfaces.Services.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Analytics;

/// <summary>
/// Google Analytics Data API v1alpha methods: quota snapshot, audience lists, recurring audience lists, report tasks and chat.
/// Alpha methods can change without notice. All endpoints require a valid Bearer access token in the Authorization header.
/// </summary>
[ApiController]
[Route("api/analytics/alpha")]
public class AnalyticsDataApiAlphaController : ControllerBase
{
    private readonly IAnalyticsDataApiService _dataApiService;

    public AnalyticsDataApiAlphaController(IAnalyticsDataApiService dataApiService)
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
    /// Get all quota usage of a property (Core, Realtime, Funnel tokens).
    /// </summary>
    [HttpGet("{propertyId}/quotas")]
    public async Task<IActionResult> GetPropertyQuotasSnapshotAsync([FromRoute] string propertyId)
    {
        var result = await _dataApiService.GetPropertyQuotasSnapshotAsync(propertyId, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// List audience lists of a property.
    /// </summary>
    [HttpGet("{propertyId}/audience-lists")]
    public async Task<IActionResult> ListAudienceListsAsync([FromRoute] string propertyId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        var result = await _dataApiService.ListAudienceListsAsync(propertyId, pageSize, pageToken, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Get one audience list.
    /// </summary>
    [HttpGet("{propertyId}/audience-lists/{audienceListId}")]
    public async Task<IActionResult> GetAudienceListAsync([FromRoute] string propertyId, [FromRoute] string audienceListId)
    {
        var result = await _dataApiService.GetAudienceListAsync(propertyId, audienceListId, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Create an audience list.
    /// </summary>
    [HttpPost("audience-lists")]
    public async Task<IActionResult> CreateAudienceListAsync([FromBody] PropertyBodyRequest request)
    {
        var result = await _dataApiService.CreateAudienceListAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Read the users in an audience list.
    /// </summary>
    [HttpPost("audience-lists/query")]
    public async Task<IActionResult> QueryAudienceListAsync([FromBody] PropertyResourceBodyRequest request)
    {
        var result = await _dataApiService.QueryAudienceListAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// List recurring audience lists of a property.
    /// </summary>
    [HttpGet("{propertyId}/recurring-audience-lists")]
    public async Task<IActionResult> ListRecurringAudienceListsAsync([FromRoute] string propertyId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        var result = await _dataApiService.ListRecurringAudienceListsAsync(propertyId, pageSize, pageToken, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Get one recurring audience list.
    /// </summary>
    [HttpGet("{propertyId}/recurring-audience-lists/{recurringAudienceListId}")]
    public async Task<IActionResult> GetRecurringAudienceListAsync([FromRoute] string propertyId, [FromRoute] string recurringAudienceListId)
    {
        var result = await _dataApiService.GetRecurringAudienceListAsync(propertyId, recurringAudienceListId, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Create a recurring audience list (refreshed daily).
    /// </summary>
    [HttpPost("recurring-audience-lists")]
    public async Task<IActionResult> CreateRecurringAudienceListAsync([FromBody] PropertyBodyRequest request)
    {
        var result = await _dataApiService.CreateRecurringAudienceListAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// List report tasks (long-running reports) of a property.
    /// </summary>
    [HttpGet("{propertyId}/report-tasks")]
    public async Task<IActionResult> ListReportTasksAsync([FromRoute] string propertyId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        var result = await _dataApiService.ListReportTasksAsync(propertyId, pageSize, pageToken, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Get one report task.
    /// </summary>
    [HttpGet("{propertyId}/report-tasks/{reportTaskId}")]
    public async Task<IActionResult> GetReportTaskAsync([FromRoute] string propertyId, [FromRoute] string reportTaskId)
    {
        var result = await _dataApiService.GetReportTaskAsync(propertyId, reportTaskId, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Create a report task (runs a large report asynchronously).
    /// </summary>
    [HttpPost("report-tasks")]
    public async Task<IActionResult> CreateReportTaskAsync([FromBody] PropertyBodyRequest request)
    {
        var result = await _dataApiService.CreateReportTaskAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Read the rows of a finished report task.
    /// </summary>
    [HttpPost("report-tasks/query")]
    public async Task<IActionResult> QueryReportTaskAsync([FromBody] PropertyResourceBodyRequest request)
    {
        var result = await _dataApiService.QueryReportTaskAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Ask a natural-language question about a property's data (v1alpha chat).
    /// </summary>
    [HttpPost("chat")]
    public async Task<IActionResult> ChatAsync([FromBody] PropertyBodyRequest request)
    {
        var result = await _dataApiService.ChatAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
