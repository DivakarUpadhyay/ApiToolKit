using ApiToolKit.Application.DTOs.Facebook.Auth;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/auth")]
[Produces("application/json")]
public class FacebookAuthController : ControllerBase
{
    private readonly IFacebookAuthService _authService;

    public FacebookAuthController(IFacebookAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>OAuth callback — receives code from Facebook redirect and exchanges it for an access token</summary>
    [HttpGet("callback")]
    public async Task<IActionResult> CallbackAsync([FromQuery] string? code, [FromQuery] string? state, [FromQuery] string? error, [FromQuery] string? error_description)
    {
        if (!string.IsNullOrEmpty(error))
        {
            var html = $"<html><body style='font-family:monospace;padding:2rem'><h2 style='color:red'>OAuth Error</h2><p><b>{error}</b>: {error_description}</p></body></html>";
            return Content(html, "text/html");
        }
        if (string.IsNullOrEmpty(code))
            return BadRequest("No authorization code received.");

        var result = await _authService.ExchangeCodeAsync(new ExchangeCodeRequest { Code = code, State = state });
        if (!result.Success)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            var errHtml = "<html><body style='font-family:monospace;padding:2rem'><h2 style='color:red'>Token Exchange Failed</h2><pre>" + json + "</pre></body></html>";
            return Content(errHtml, "text/html");
        }

        var token = result.Data?.AccessToken ?? string.Empty;
        var redirectUrl = $"http://localhost:3000/auth/callback?token={Uri.EscapeDataString(token)}";
        return Redirect(redirectUrl);
    }

    /// <summary>Get Facebook OAuth authorization URL</summary>
    [HttpPost("auth-url")]
    public async Task<IActionResult> GetAuthUrlAsync([FromBody] GetAuthUrlRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.GetAuthUrlAsync(request);
        return Ok(result);
    }

    /// <summary>Exchange authorization code for access token</summary>
    [HttpPost("token")]
    public async Task<IActionResult> ExchangeCodeAsync([FromBody] ExchangeCodeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.ExchangeCodeAsync(request);
        return Ok(result);
    }

    /// <summary>Get app-level access token</summary>
    [HttpGet("app-token")]
    public async Task<IActionResult> GetAppAccessTokenAsync()
    {
        var result = await _authService.GetAppAccessTokenAsync();
        return Ok(result);
    }

    /// <summary>Exchange short-lived token for a long-lived token</summary>
    [HttpPost("token/long-lived")]
    public async Task<IActionResult> GetLongLivedTokenAsync([FromBody] LongLivedTokenRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.GetLongLivedTokenAsync(request);
        return Ok(result);
    }

    /// <summary>Debug and inspect an access token</summary>
    [HttpPost("token/debug")]
    public async Task<IActionResult> DebugTokenAsync([FromBody] DebugTokenRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.DebugTokenAsync(request);
        return Ok(result);
    }

    /// <summary>Refresh a long-lived token</summary>
    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.RefreshTokenAsync(request);
        return Ok(result);
    }
}
