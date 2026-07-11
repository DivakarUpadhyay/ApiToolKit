using ApiToolKit.Application.DTOs.Identity.OAuth;
using ApiToolKit.Application.Interfaces.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Identity;

[ApiController]
[Route("api/identity/oauth")]
public class IdentityOAuthController : ControllerBase
{
    private readonly IIdentityOAuthService _oauthService;

    public IdentityOAuthController(IIdentityOAuthService oauthService)
    {
        _oauthService = oauthService;
    }

    /// <summary>Build the Google OAuth 2.0 authorization URL.</summary>
    [HttpGet("url")]
    public async Task<IActionResult> GetAuthUrl([FromQuery] string scopes = "openid email profile", [FromQuery] string? state = null)
    {
        var request = new AuthorizationRequest
        {
            Scopes = scopes.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
            State = state
        };
        var result = await _oauthService.GetAuthUrlAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Handle the OAuth 2.0 callback and exchange the authorization code for tokens.</summary>
    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string? redirectUri = null)
    {
        var result = await _oauthService.ExchangeCodeAsync(code, redirectUri ?? string.Empty);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Refresh an access token using a refresh token.</summary>
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _oauthService.RefreshTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Revoke an access or refresh token.</summary>
    [HttpPost("revoke")]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest request)
    {
        var result = await _oauthService.RevokeTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get information about an ID token or access token.</summary>
    [HttpGet("token-info")]
    public async Task<IActionResult> GetTokenInfo([FromQuery] string accessToken)
    {
        var result = await _oauthService.GetTokenInfoAsync(accessToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get user info using an access token (OpenID Connect userinfo endpoint).</summary>
    [HttpGet("userinfo")]
    public async Task<IActionResult> GetUserInfo([FromHeader(Name = "Authorization")] string authorization)
    {
        var accessToken = authorization.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();
        var result = await _oauthService.GetUserInfoAsync(accessToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
