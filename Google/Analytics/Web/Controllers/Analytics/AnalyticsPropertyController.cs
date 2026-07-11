using ApiToolKit.Application.Interfaces.Services.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Analytics;

/// <summary>
/// Property and Account management endpoints using the Google Analytics Admin API.
/// </summary>
[ApiController]
[Route("api/analytics/properties")]
public class AnalyticsPropertyController : ControllerBase
{
    private readonly IAnalyticsPropertyService _propertyService;

    public AnalyticsPropertyController(IAnalyticsPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    private string GetAccessToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        return authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
            ? authHeader["Bearer ".Length..]
            : authHeader;
    }

    /// <summary>
    /// List all Google Analytics accounts accessible to the authenticated user.
    /// </summary>
    [HttpGet("accounts")]
    public async Task<IActionResult> ListAccountsAsync()
    {
        var result = await _propertyService.ListAccountsAsync(GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// List all GA4 properties under a specific account.
    /// </summary>
    [HttpGet("list")]
    public async Task<IActionResult> ListPropertiesAsync([FromQuery] string accountName)
    {
        var result = await _propertyService.ListPropertiesAsync(accountName, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Get detailed information for a specific GA4 property by its resource name (e.g. "properties/123456").
    /// </summary>
    [HttpGet("{propertyId}")]
    public async Task<IActionResult> GetPropertyAsync([FromRoute] string propertyId)
    {
        var propertyName = propertyId.StartsWith("properties/") ? propertyId : $"properties/{propertyId}";
        var result = await _propertyService.GetPropertyAsync(propertyName, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// List all data streams (web, Android, iOS) for a GA4 property.
    /// </summary>
    [HttpGet("{propertyId}/streams")]
    public async Task<IActionResult> ListDataStreamsAsync([FromRoute] string propertyId)
    {
        var result = await _propertyService.ListDataStreamsAsync(propertyId, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
