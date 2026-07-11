using ApiToolKit.Application.DTOs.Firebase.Auth;
using ApiToolKit.Application.Interfaces.Services.Firebase;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Firebase;

[ApiController]
[Route("api/firebase/auth")]
public class FirebaseOAuthController : ControllerBase
{
    private readonly IFirebaseAuthService2 _authService;

    public FirebaseOAuthController(IFirebaseAuthService2 authService)
    {
        _authService = authService;
    }

    /// <summary>Get the Google OAuth 2.0 authorization URL for Firebase scopes.</summary>
    [HttpGet("url")]
    public async Task<IActionResult> GetAuthUrl()
    {
        var result = await _authService.GetAuthUrlAsync();
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Handle the OAuth 2.0 callback and exchange the authorization code for Firebase tokens.</summary>
    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string redirectUri)
    {
        var result = await _authService.ExchangeCodeAsync(new ExchangeCodeRequest { Code = code, RedirectUri = redirectUri });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Refresh a Firebase OAuth access token using a refresh token.</summary>
    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
