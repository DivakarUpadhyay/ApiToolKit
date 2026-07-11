using ApiToolKit.Application.DTOs.Analytics.Auth;
using ApiToolKit.Application.Interfaces.Services.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Analytics;

/// <summary>
/// OAuth 2.0 Authentication endpoints for Google Analytics (GA4).
/// </summary>
[ApiController]
[Route("api/analytics/auth")]
public class AnalyticsAuthController : ControllerBase
{
    private readonly IAnalyticsAuthService _authService;

    public AnalyticsAuthController(IAnalyticsAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Get the Google OAuth 2.0 authorization URL for GA4 access.
    /// </summary>
    [HttpGet("url")]
    public IActionResult GetAuthorizationUrl()
    {
        var result = _authService.GetAuthorizationUrl();
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// OAuth 2.0 callback endpoint. Exchange authorization code for access and refresh tokens.
    /// </summary>
    [HttpGet("callback")]
    public async Task<IActionResult> CallbackAsync([FromQuery] string code, [FromQuery] string propertyId)
    {
        if (string.IsNullOrEmpty(code))
            return BadRequest("Authorization code is required.");

        var request = new ExchangeCodeRequest { Code = code, PropertyId = propertyId };
        var result = await _authService.ExchangeCodeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Refresh an expired access token using the stored refresh token for a given property.
    /// </summary>
    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
