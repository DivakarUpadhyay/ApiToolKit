using ApiToolKit.Application.DTOs.BigQuery.Auth;
using ApiToolKit.Application.Interfaces.Services.BigQuery;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BigQuery;

[ApiController]
[Route("api/bigquery/auth")]
public class BigQueryAuthController : ControllerBase
{
    private readonly IBigQueryAuthService _authService;

    public BigQueryAuthController(IBigQueryAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Get the OAuth 2.0 authorization URL for BigQuery.</summary>
    [HttpGet("url")]
    public async Task<IActionResult> GetAuthUrl()
    {
        var result = await _authService.GetAuthUrlAsync();
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Handle OAuth 2.0 callback and exchange code for tokens.</summary>
    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string redirectUri)
    {
        if (string.IsNullOrEmpty(code))
            return BadRequest("Authorization code is required.");

        var request = new ExchangeCodeRequest
        {
            Code = code,
            RedirectUri = redirectUri ?? string.Empty
        };
        var result = await _authService.ExchangeCodeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Refresh an access token using a refresh token.</summary>
    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.RefreshTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
