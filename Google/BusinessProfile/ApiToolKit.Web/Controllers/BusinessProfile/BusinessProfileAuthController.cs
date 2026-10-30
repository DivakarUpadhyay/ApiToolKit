using ApiToolKit.Application.DTOs.BusinessProfile.Auth;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BusinessProfile;

/// <summary>
/// OAuth 2.0 authentication endpoints for the Google Business Profile APIs.
/// </summary>
[ApiController]
[Route("api/businessprofile/auth")]
public class BusinessProfileAuthController : ControllerBase
{
    private readonly IBusinessProfileAuthService _authService;

    public BusinessProfileAuthController(IBusinessProfileAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Get the Google OAuth 2.0 consent URL. accountKey is any name you choose for this Google account; reuse it on every other endpoint.
    /// </summary>
    [HttpGet("url")]
    public IActionResult GetAuthorizationUrl([FromQuery] string accountKey)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var result = _authService.GetAuthorizationUrl(accountKey);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// OAuth 2.0 callback. Google sends the code and the accountKey (as state); tokens are stored for that account key.
    /// </summary>
    [HttpGet("callback")]
    public async Task<IActionResult> CallbackAsync([FromQuery] string code, [FromQuery] string state)
    {
        if (string.IsNullOrEmpty(code)) return BadRequest("Authorization code is required.");
        if (string.IsNullOrEmpty(state)) return BadRequest("state (accountKey) is required.");

        var result = await _authService.ExchangeCodeAsync(code, state);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Refresh the stored access token for an account key.
    /// </summary>
    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.RefreshTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
