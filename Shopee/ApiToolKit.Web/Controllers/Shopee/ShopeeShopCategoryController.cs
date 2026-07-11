using ApiToolKit.Application.DTOs.Shopee.ShopCategory;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/shop-category")]
[Produces("application/json")]
public class ShopeeShopCategoryController : ControllerBase
{
    private readonly IShopeeShopCategoryService _shopCategoryService;

    public ShopeeShopCategoryController(IShopeeShopCategoryService shopCategoryService)
    {
        _shopCategoryService = shopCategoryService;
    }

    /// <summary>Create a new shop category</summary>
    [HttpPost]
    public async Task<IActionResult> AddShopCategory([FromBody] AddShopCategoryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopCategoryService.AddShopCategoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of shop categories</summary>
    [HttpGet("list")]
    public async Task<IActionResult> GetShopCategoryList([FromQuery] GetShopCategoryListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopCategoryService.GetShopCategoryListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a shop category</summary>
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteShopCategory([FromBody] DeleteShopCategoryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopCategoryService.DeleteShopCategoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing shop category</summary>
    [HttpPost("update")]
    public async Task<IActionResult> UpdateShopCategory([FromBody] UpdateShopCategoryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopCategoryService.UpdateShopCategoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add items to a shop category</summary>
    [HttpPost("items")]
    public async Task<IActionResult> AddItemsOfShopCategory([FromBody] AddItemsOfShopCategoryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopCategoryService.AddItemsOfShopCategoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get items belonging to a shop category</summary>
    [HttpGet("items")]
    public async Task<IActionResult> GetItemsOfShopCategory([FromQuery] GetItemsOfShopCategoryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopCategoryService.GetItemsOfShopCategoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove items from a shop category</summary>
    [HttpPost("items/delete")]
    public async Task<IActionResult> DeleteItemsOfShopCategory([FromBody] DeleteItemsOfShopCategoryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopCategoryService.DeleteItemsOfShopCategoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
