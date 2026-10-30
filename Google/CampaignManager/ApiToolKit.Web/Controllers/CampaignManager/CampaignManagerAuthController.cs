using ApiToolKit.Application.DTOs.CampaignManager.Auth;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// OAuth 2.0 authentication endpoints for the Campaign Manager 360 API.
/// </summary>
[ApiController]
[Route("api/campaignmanager/auth")]
public class CampaignManagerAuthController : ControllerBase
{
    private readonly ICampaignManagerAuthService _authService;

    public CampaignManagerAuthController(ICampaignManagerAuthService authService)
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
