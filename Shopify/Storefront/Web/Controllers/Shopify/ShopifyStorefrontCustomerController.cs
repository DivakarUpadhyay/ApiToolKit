using ApiToolKit.Application.DTOs.Shopify.Storefront;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

[ApiController]
[Route("api/shopify/storefront/customer")]
[Produces("application/json")]
public class ShopifyStorefrontCustomerController : ControllerBase
{
    private readonly IShopifyStorefrontCustomerService _customerService;
    public ShopifyStorefrontCustomerController(IShopifyStorefrontCustomerService customerService) { _customerService = customerService; }

    /// <summary>Customer login - get access token</summary>
    [HttpPost("login")]
    public async Task<IActionResult> CustomerLoginAsync([FromQuery] string shopDomain, [FromBody] CustomerLoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _customerService.CustomerLoginAsync(shopDomain, request.Email, request.Password);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Customer register</summary>
    [HttpPost("register")]
    public async Task<IActionResult> CustomerRegisterAsync([FromQuery] string shopDomain, [FromBody] RegisterCustomerRequest request)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _customerService.CustomerRegisterAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get customer info using access token</summary>
    [HttpGet]
    public async Task<IActionResult> GetCustomerAsync([FromQuery] string shopDomain, [FromQuery] string customerAccessToken)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        if (string.IsNullOrWhiteSpace(customerAccessToken)) return BadRequest("customerAccessToken is required.");
        var result = await _customerService.GetCustomerAsync(shopDomain, customerAccessToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Customer logout - delete access token</summary>
    [HttpDelete("logout")]
    public async Task<IActionResult> CustomerLogoutAsync([FromQuery] string shopDomain, [FromQuery] string customerAccessToken)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        if (string.IsNullOrWhiteSpace(customerAccessToken)) return BadRequest("customerAccessToken is required.");
        var result = await _customerService.CustomerLogoutAsync(shopDomain, customerAccessToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
