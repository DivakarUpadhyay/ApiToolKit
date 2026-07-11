using ApiToolKit.Application.DTOs.Shopify.Auth;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

[ApiController]
[Route("api/shopify/auth")]
[Produces("application/json")]
public class ShopifyAuthController : ControllerBase
{
    private readonly IShopifyAuthService _authService;

    public ShopifyAuthController(IShopifyAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Generate Shopify app install/authorization URL</summary>
    [HttpPost("install-url")]
    public async Task<IActionResult> GetInstallUrlAsync([FromBody] GetInstallUrlRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.GetInstallUrlAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Handle OAuth callback - verify HMAC and exchange code for access token</summary>
    [HttpPost("callback")]
    public async Task<IActionResult> HandleCallbackAsync([FromBody] CallbackRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.HandleCallbackAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get shop information using stored access token</summary>
    [HttpGet("shop")]
    public async Task<IActionResult> GetShopInfoAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _authService.GetShopInfoAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Verify Shopify HMAC signature from query string</summary>
    [HttpPost("verify-hmac")]
    public async Task<IActionResult> VerifyHmacAsync([FromBody] VerifyHmacRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _authService.VerifyHmacAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get stored access token for a shop</summary>
    [HttpGet("token")]
    public async Task<IActionResult> GetStoredTokenAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _authService.GetStoredTokenAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Revoke stored access token for a shop</summary>
    [HttpDelete("token")]
    public async Task<IActionResult> RevokeTokenAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _authService.RevokeTokenAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
