using ApiToolKit.Application.DTOs.Shopee.ShopFlashSale;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/shop-flash-sale")]
[Produces("application/json")]
public class ShopeeShopFlashSaleController : ControllerBase
{
    private readonly IShopeeShopFlashSaleService _shopFlashSaleService;

    public ShopeeShopFlashSaleController(IShopeeShopFlashSaleService shopFlashSaleService)
    {
        _shopFlashSaleService = shopFlashSaleService;
    }

    /// <summary>Get available flash sale time slot ID</summary>
    [HttpPost("get-time-slot-id")]
    public async Task<IActionResult> GetTimeSlotId([FromBody] GetTimeSlotIdRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopFlashSaleService.GetTimeSlotIdAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new shop flash sale</summary>
    [HttpPost("create")]
    public async Task<IActionResult> CreateFlashSale([FromBody] CreateFlashSaleRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopFlashSaleService.CreateFlashSaleAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add items to a flash sale</summary>
    [HttpPost("items")]
    public async Task<IActionResult> AddFlashSaleItems([FromBody] AddFlashSaleItemsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopFlashSaleService.AddFlashSaleItemsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a flash sale status or details</summary>
    [HttpPost("update")]
    public async Task<IActionResult> UpdateFlashSale([FromBody] UpdateFlashSaleRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopFlashSaleService.UpdateFlashSaleAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a flash sale</summary>
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteFlashSale([FromBody] DeleteFlashSaleRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopFlashSaleService.DeleteFlashSaleAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of flash sales</summary>
    [HttpGet("list")]
    public async Task<IActionResult> GetFlashSaleList([FromQuery] GetFlashSaleListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopFlashSaleService.GetFlashSaleListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get details of a specific flash sale</summary>
    [HttpGet("{flashSaleId}")]
    public async Task<IActionResult> GetFlashSale(long flashSaleId)
    {
        var result = await _shopFlashSaleService.GetFlashSaleAsync(new GetFlashSaleRequest { FlashSaleId = flashSaleId });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update items within a flash sale</summary>
    [HttpPost("items/update")]
    public async Task<IActionResult> UpdateFlashSaleItems([FromBody] UpdateFlashSaleItemsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopFlashSaleService.UpdateFlashSaleItemsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete items from a flash sale</summary>
    [HttpPost("items/delete")]
    public async Task<IActionResult> DeleteFlashSaleItems([FromBody] DeleteFlashSaleItemsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopFlashSaleService.DeleteFlashSaleItemsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get items in a flash sale</summary>
    [HttpGet("items")]
    public async Task<IActionResult> GetFlashSaleItems([FromQuery] GetFlashSaleItemsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopFlashSaleService.GetFlashSaleItemsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get item eligibility criteria for flash sale</summary>
    [HttpGet("item-criteria")]
    public async Task<IActionResult> GetItemCriteria([FromQuery] GetItemCriteriaRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopFlashSaleService.GetItemCriteriaAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
