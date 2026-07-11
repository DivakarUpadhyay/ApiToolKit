using ApiToolKit.Application.DTOs.Shopify.Storefront;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

[ApiController]
[Route("api/shopify/storefront/cart")]
[Produces("application/json")]
public class ShopifyCartController : ControllerBase
{
    private readonly IShopifyCartService _cartService;
    public ShopifyCartController(IShopifyCartService cartService) { _cartService = cartService; }

    /// <summary>Create a new cart</summary>
    [HttpPost]
    public async Task<IActionResult> CreateCartAsync([FromQuery] string shopDomain, [FromBody] CreateCartRequest request)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _cartService.CreateCartAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get cart by ID</summary>
    [HttpGet("{cartId}")]
    public async Task<IActionResult> GetCartAsync([FromQuery] string shopDomain, string cartId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _cartService.GetCartAsync(shopDomain, cartId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add lines to cart</summary>
    [HttpPost("{cartId}/lines")]
    public async Task<IActionResult> AddCartLinesAsync([FromQuery] string shopDomain, string cartId, [FromBody] AddCartLinesRequest request)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _cartService.AddCartLinesAsync(shopDomain, cartId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update cart lines</summary>
    [HttpPut("{cartId}/lines")]
    public async Task<IActionResult> UpdateCartLinesAsync([FromQuery] string shopDomain, string cartId, [FromBody] UpdateCartLinesRequest request)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _cartService.UpdateCartLinesAsync(shopDomain, cartId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove lines from cart</summary>
    [HttpDelete("{cartId}/lines")]
    public async Task<IActionResult> RemoveCartLinesAsync([FromQuery] string shopDomain, string cartId, [FromBody] RemoveCartLinesRequest request)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _cartService.RemoveCartLinesAsync(shopDomain, cartId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update cart buyer identity</summary>
    [HttpPut("{cartId}/buyer-identity")]
    public async Task<IActionResult> UpdateCartBuyerIdentityAsync([FromQuery] string shopDomain, string cartId, [FromBody] CartBuyerIdentityRequest request)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _cartService.UpdateCartBuyerIdentityAsync(shopDomain, cartId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
