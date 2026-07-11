using ApiToolKit.Application.DTOs.Cloud.Auth;
using ApiToolKit.Application.Interfaces.Services.Cloud;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Cloud;

/// <summary>
/// Google Cloud OAuth 2.0 authentication endpoints.
/// </summary>
[ApiController]
[Route("api/cloud/auth")]
public class CloudAuthController : ControllerBase
{
    private readonly ICloudAuthService _authService;

    public CloudAuthController(ICloudAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Returns the Google OAuth 2.0 authorization URL to redirect the user to.
    /// </summary>
    [HttpGet("url")]
    public async Task<IActionResult> GetAuthUrl()
    {
        var result = await _authService.GetAuthUrlAsync();
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// OAuth 2.0 callback endpoint. Exchanges the authorization code for tokens.
    /// </summary>
    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string? redirectUri)
    {
        if (string.IsNullOrEmpty(code))
            return BadRequest(new { Message = "Authorization code is required." });

        var request = new ExchangeCodeRequest
        {
            Code = code,
            RedirectUri = redirectUri ?? $"{Request.Scheme}://{Request.Host}/api/cloud/auth/callback"
        };

        var result = await _authService.ExchangeCodeAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Refreshes an expired access token using a refresh token.
    /// </summary>
    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _authService.RefreshTokenAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }
}
