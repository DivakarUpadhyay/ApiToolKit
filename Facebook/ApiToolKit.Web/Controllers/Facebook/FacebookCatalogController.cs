using ApiToolKit.Application.DTOs.Facebook.Catalog;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/catalog")]
[Produces("application/json")]
public class FacebookCatalogController : ControllerBase
{
    private readonly IFacebookCatalogService _catalogService;

    public FacebookCatalogController(IFacebookCatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    /// <summary>Create a new product catalog</summary>
    [HttpPost]
    public async Task<IActionResult> CreateCatalogAsync([FromBody] CreateCatalogRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _catalogService.CreateCatalogAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get catalog details by catalog ID</summary>
    [HttpGet("{catalogId}")]
    public async Task<IActionResult> GetCatalogAsync(string catalogId, [FromQuery] string accessToken)
    {
        var result = await _catalogService.GetCatalogAsync(catalogId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all catalogs owned by a business</summary>
    [HttpPost("list")]
    public async Task<IActionResult> GetCatalogsAsync([FromBody] GetCatalogsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _catalogService.GetCatalogsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a catalog</summary>
    [HttpDelete("{catalogId}")]
    public async Task<IActionResult> DeleteCatalogAsync(string catalogId, [FromQuery] string accessToken)
    {
        var result = await _catalogService.DeleteCatalogAsync(catalogId, accessToken);
        return Ok(result);
    }

    /// <summary>Create a product in a catalog</summary>
    [HttpPost("{catalogId}/product")]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _catalogService.CreateProductAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get a product by its ID</summary>
    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetProductAsync(string productId, [FromQuery] string accessToken)
    {
        var result = await _catalogService.GetProductAsync(productId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all products in a catalog</summary>
    [HttpPost("{catalogId}/products")]
    public async Task<IActionResult> GetProductsAsync([FromBody] GetProductsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _catalogService.GetProductsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Update a product</summary>
    [HttpPut("product/{productId}")]
    public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _catalogService.UpdateProductAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a product</summary>
    [HttpDelete("product/{productId}")]
    public async Task<IActionResult> DeleteProductAsync(string productId, [FromQuery] string accessToken)
    {
        var result = await _catalogService.DeleteProductAsync(productId, accessToken);
        return Ok(result);
    }

    /// <summary>Create a product set inside a catalog</summary>
    [HttpPost("{catalogId}/product-set")]
    public async Task<IActionResult> CreateProductSetAsync([FromBody] CreateProductSetRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _catalogService.CreateProductSetAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get all product sets in a catalog</summary>
    [HttpGet("{catalogId}/product-sets")]
    public async Task<IActionResult> GetProductSetsAsync(string catalogId, [FromQuery] string accessToken)
    {
        var result = await _catalogService.GetProductSetsAsync(catalogId, accessToken);
        return Ok(result);
    }
}
