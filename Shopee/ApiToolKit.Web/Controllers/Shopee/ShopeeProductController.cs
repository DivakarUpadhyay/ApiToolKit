using ApiToolKit.Application.DTOs.Shopee.Product;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/product")]
[Produces("application/json")]
public class ShopeeProductController : ControllerBase
{
    private readonly IShopeeProductService _productService;

    public ShopeeProductController(IShopeeProductService productService)
    {
        _productService = productService;
    }

    /// <summary>Get list of items in a shop</summary>
    [HttpGet("list")]
    public async Task<IActionResult> GetItemList([FromQuery] GetItemListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productService.GetItemListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get base info of specific items</summary>
    [HttpGet("info")]
    public async Task<IActionResult> GetItemBaseInfo([FromQuery] GetItemBaseInfoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productService.GetItemBaseInfoAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add a new item to shop</summary>
    [HttpPost]
    public async Task<IActionResult> AddItem([FromBody] AddItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productService.AddItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing item</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateItem([FromBody] UpdateItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productService.UpdateItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update item price</summary>
    [HttpPut("price")]
    public async Task<IActionResult> UpdateItemPrice([FromBody] UpdatePriceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productService.UpdateItemPriceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update item stock</summary>
    [HttpPut("stock")]
    public async Task<IActionResult> UpdateItemStock([FromBody] UpdateStockRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productService.UpdateItemStockAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete items from shop</summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteItem([FromBody] DeleteItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productService.DeleteItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get Shopee category tree</summary>
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategory([FromQuery] GetCategoryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productService.GetCategoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get attributes for a category</summary>
    [HttpGet("attributes")]
    public async Task<IActionResult> GetAttributes([FromQuery] GetAttributesRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productService.GetAttributesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get brands for a category</summary>
    [HttpGet("brands")]
    public async Task<IActionResult> GetBrands([FromQuery] long shopId, [FromQuery] long categoryId, [FromQuery] string language = "en")
    {
        var result = await _productService.GetBrandsAsync(shopId, categoryId, language);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get model list (variants) of an item</summary>
    [HttpGet("models")]
    public async Task<IActionResult> GetModelList([FromQuery] long shopId, [FromQuery] long itemId)
    {
        var result = await _productService.GetModelListAsync(shopId, itemId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Initialize tier variation for an item</summary>
    [HttpPost("tier-variation")]
    public async Task<IActionResult> InitTierVariation([FromQuery] long shopId, [FromQuery] long itemId, [FromBody] object tierVariation)
    {
        var result = await _productService.InitTierVariationAsync(shopId, itemId, tierVariation);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
