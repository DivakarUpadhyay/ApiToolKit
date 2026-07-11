using ApiToolKit.Application.DTOs.Shopee.GlobalProduct;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/global-product")]
[Produces("application/json")]
public class ShopeeGlobalProductController : ControllerBase
{
    private readonly IShopeeGlobalProductService _globalProductService;

    public ShopeeGlobalProductController(IShopeeGlobalProductService globalProductService)
    {
        _globalProductService = globalProductService;
    }

    /// <summary>Get global category list</summary>
    [HttpGet("categories")]
    public async Task<IActionResult> GetGlobalCategoryList([FromQuery] GetGlobalCategoryListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.GetGlobalCategoryListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get attributes for a global category</summary>
    [HttpGet("attributes")]
    public async Task<IActionResult> GetCategoryAttribute([FromQuery] GetCategoryAttributeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.GetCategoryAttributeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get brand list for a global category</summary>
    [HttpGet("brands")]
    public async Task<IActionResult> GetBrandList([FromQuery] GetBrandListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.GetBrandListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add a new global item</summary>
    [HttpPost("item")]
    public async Task<IActionResult> AddGlobalItem([FromBody] AddGlobalItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.AddGlobalItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Copy a global item to one or more shops</summary>
    [HttpPost("item/copy")]
    public async Task<IActionResult> CopyItem([FromBody] CopyItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.CopyItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete global items</summary>
    [HttpPost("item/delete")]
    public async Task<IActionResult> DeleteGlobalItem([FromBody] DeleteGlobalItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.DeleteGlobalItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of global items</summary>
    [HttpGet("items")]
    public async Task<IActionResult> GetGlobalItemList([FromQuery] GetGlobalItemListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.GetGlobalItemListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get detail of global items by ID list</summary>
    [HttpPost("item/detail")]
    public async Task<IActionResult> GetGlobalItemDetail([FromBody] GetGlobalItemDetailRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.GetGlobalItemDetailAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a global item</summary>
    [HttpPost("item/update")]
    public async Task<IActionResult> UpdateGlobalItem([FromBody] UpdateGlobalItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.UpdateGlobalItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Search global items by keyword</summary>
    [HttpGet("items/search")]
    public async Task<IActionResult> SearchGlobalItem([FromQuery] SearchGlobalItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.SearchGlobalItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Initialize tier variation for a global item</summary>
    [HttpPost("item/tier-variation/init")]
    public async Task<IActionResult> InitTierVariation([FromBody] InitTierVariationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.InitTierVariationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update tier variation for a global item</summary>
    [HttpPost("item/tier-variation/update")]
    public async Task<IActionResult> UpdateTierVariation([FromBody] UpdateTierVariationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.UpdateTierVariationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get model list for a global item</summary>
    [HttpGet("item/models")]
    public async Task<IActionResult> GetModelList([FromQuery] GetModelListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.GetModelListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update models for a global item</summary>
    [HttpPost("item/models/update")]
    public async Task<IActionResult> UpdateModel([FromBody] UpdateModelRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.UpdateModelAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add models to a global item</summary>
    [HttpPost("item/models/add")]
    public async Task<IActionResult> AddGlobalModel([FromBody] AddGlobalModelRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.AddGlobalModelAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete models from a global item</summary>
    [HttpPost("item/models/delete")]
    public async Task<IActionResult> DeleteGlobalModel([FromBody] DeleteGlobalModelRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.DeleteGlobalModelAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get global item info</summary>
    [HttpGet("item/info")]
    public async Task<IActionResult> GetGlobalItemInfo([FromQuery] GetGlobalItemInfoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.GetGlobalItemInfoAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update the status of global items</summary>
    [HttpPost("item/status")]
    public async Task<IActionResult> UpdateGlobalItemStatus([FromBody] UpdateGlobalItemStatusRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.UpdateGlobalItemStatusAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get shop-level item info linked to a global item</summary>
    [HttpGet("item/shop-info")]
    public async Task<IActionResult> GetShopItemInfo([FromQuery] GetShopItemInfoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.GetShopItemInfoAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get logistics channel list for a global item</summary>
    [HttpGet("item/channels")]
    public async Task<IActionResult> GetChannelList([FromQuery] GetChannelListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.GetChannelListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update logistic info for a global item</summary>
    [HttpPost("item/logistic")]
    public async Task<IActionResult> UpdateGlobalItemLogistic([FromBody] UpdateGlobalItemLogisticRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.UpdateGlobalItemLogisticAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Batch update price for global items</summary>
    [HttpPost("items/batch-price")]
    public async Task<IActionResult> BatchUpdatePrice([FromBody] BatchUpdatePriceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.BatchUpdatePriceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Batch update stock for global items</summary>
    [HttpPost("items/batch-stock")]
    public async Task<IActionResult> BatchUpdateStock([FromBody] BatchUpdateStockRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _globalProductService.BatchUpdateStockAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
