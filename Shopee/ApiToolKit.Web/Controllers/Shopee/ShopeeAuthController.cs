using ApiToolKit.Application.DTOs.Shopee.Auth;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/auth")]
[Produces("application/json")]
public class ShopeeAuthController : ControllerBase
{
    private readonly IShopeeAuthService _authService;

    public ShopeeAuthController(IShopeeAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Get Shopee OAuth authorization URL</summary>
    [HttpGet("auth-url")]
    public async Task<IActionResult> GetAuthUrl()
    {
        var result = await _authService.GetAuthUrlAsync();
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Exchange authorization code for access token</summary>
    [HttpPost("token")]
    public async Task<IActionResult> GetAccessToken([FromBody] GetAccessTokenRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.GetAccessTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Refresh expired access token using refresh token</summary>
    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshAccessTokenRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.RefreshAccessTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
