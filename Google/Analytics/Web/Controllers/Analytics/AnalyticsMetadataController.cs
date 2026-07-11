using ApiToolKit.Application.Interfaces.Services.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Analytics;

/// <summary>
/// Metadata endpoints for Google Analytics (GA4) — lists available dimensions and metrics.
/// </summary>
[ApiController]
[Route("api/analytics/metadata")]
public class AnalyticsMetadataController : ControllerBase
{
    private readonly IAnalyticsMetadataService _metadataService;

    public AnalyticsMetadataController(IAnalyticsMetadataService metadataService)
    {
        _metadataService = metadataService;
    }

    private string GetAccessToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        return authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
            ? authHeader["Bearer ".Length..]
            : authHeader;
    }

    /// <summary>
    /// Get all available dimensions and metrics metadata for a GA4 property.
    /// Use "GA4:propertyId" format or "properties/{propertyId}".
    /// </summary>
    [HttpGet("{propertyId}")]
    public async Task<IActionResult> GetMetadataAsync([FromRoute] string propertyId)
    {
        var result = await _metadataService.GetMetadataAsync(propertyId, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
