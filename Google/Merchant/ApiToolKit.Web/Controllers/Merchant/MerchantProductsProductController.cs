using ApiToolKit.Application.DTOs.Merchant.ProductsProduct;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// products (v1)
/// </summary>
[ApiController]
[Route("api/merchant/products")]
[Produces("application/json")]
public class MerchantProductsProductController : ControllerBase
{
    private readonly IMerchantProductsProductService _productsProductService;

    public MerchantProductsProductController(IMerchantProductsProductService productsProductService)
    {
        _productsProductService = productsProductService;
    }

    /// <summary>Lists the processed products in your Merchant Center account.</summary>
    [HttpGet("accounts/{accountId}/products")]
    public async Task<IActionResult> ListProductsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListProductsRequest { AccountKey = accountKey, AccountId = accountId, PageToken = pageToken, PageSize = pageSize };
        var result = await _productsProductService.ListProductsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves the processed product from your Merchant Center account.</summary>
    [HttpGet("accounts/{accountId}/products/{productId}")]
    public async Task<IActionResult> GetProductAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string productId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetProductRequest { AccountKey = accountKey, AccountId = accountId, ProductId = productId };
        var result = await _productsProductService.GetProductAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
