using ApiToolKit.Application.DTOs.GoogleAds.Auth;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.GoogleAds;

[ApiController]
[Route("api/googleads/auth")]
[Produces("application/json")]
public class GoogleAdsAuthController : ControllerBase
{
    private readonly IGoogleAdsAuthService _authService;

    public GoogleAdsAuthController(IGoogleAdsAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Get Google OAuth authorization URL (scope: https://www.googleapis.com/auth/adwords)</summary>
    [HttpGet("url")]
    public async Task<IActionResult> GetAuthUrl()
    {
        var result = await _authService.GetAuthUrlAsync();
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Exchange authorization code for access and refresh tokens</summary>
    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string? redirectUri = null)
    {
        if (string.IsNullOrEmpty(code))
            return BadRequest("Code is required.");

        var request = new ExchangeCodeRequest
        {
            Code = code,
            RedirectUri = redirectUri ?? "https://localhost:5001/api/googleads/auth/callback"
        };
        var result = await _authService.ExchangeCodeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Refresh access token using a refresh token</summary>
    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.RefreshTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
