using ApiToolKit.Application.DTOs.YouTube.Auth;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.YouTube;

[ApiController]
[Route("api/youtube/auth")]
[Produces("application/json")]
public class YouTubeAuthController : ControllerBase
{
    private readonly IYouTubeAuthService _authService;

    public YouTubeAuthController(IYouTubeAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Get the OAuth2 authorization URL</summary>
    [HttpGet("url")]
    public IActionResult GetAuthUrl([FromQuery] string[]? scopes)
    {
        var defaultScopes = scopes != null && scopes.Length > 0
            ? scopes
            : new[]
            {
                "https://www.googleapis.com/auth/youtube",
                "https://www.googleapis.com/auth/youtube.readonly",
                "https://www.googleapis.com/auth/yt-analytics.readonly"
            };

        var result = _authService.GetAuthorizationUrl(defaultScopes);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Handle OAuth2 callback and exchange authorization code for tokens</summary>
    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string? userId = "default")
    {
        if (string.IsNullOrEmpty(code)) return BadRequest("Authorization code is missing.");
        var request = new ExchangeCodeRequest { Code = code, RedirectUri = $"{Request.Scheme}://{Request.Host}/api/youtube/auth/callback" };
        var result = await _authService.ExchangeCodeAsync(request, userId ?? "default");
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Refresh an access token using a refresh token</summary>
    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.RefreshTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
