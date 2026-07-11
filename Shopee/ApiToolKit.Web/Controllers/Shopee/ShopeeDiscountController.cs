using ApiToolKit.Application.DTOs.Shopee.Discount;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/discount")]
[Produces("application/json")]
public class ShopeeDiscountController : ControllerBase
{
    private readonly IShopeeDiscountService _discountService;

    public ShopeeDiscountController(IShopeeDiscountService discountService)
    {
        _discountService = discountService;
    }

    /// <summary>Create a new discount promotion</summary>
    [HttpPost]
    public async Task<IActionResult> AddDiscount([FromBody] AddDiscountRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _discountService.AddDiscountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a discount promotion</summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteDiscount([FromBody] DeleteDiscountRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _discountService.DeleteDiscountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a discount promotion</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateDiscount([FromBody] UpdateDiscountRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _discountService.UpdateDiscountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>End a discount promotion early</summary>
    [HttpPost("end")]
    public async Task<IActionResult> EndDiscount([FromBody] EndDiscountRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _discountService.EndDiscountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get details of a specific discount</summary>
    [HttpGet("{shopId}/{discountId}")]
    public async Task<IActionResult> GetDiscount(long shopId, long discountId)
    {
        var result = await _discountService.GetDiscountAsync(new GetDiscountRequest { ShopId = shopId, DiscountId = discountId });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of discounts for a shop</summary>
    [HttpGet("list")]
    public async Task<IActionResult> GetDiscountList([FromQuery] GetDiscountListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _discountService.GetDiscountListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add items to an existing discount</summary>
    [HttpPost("items")]
    public async Task<IActionResult> AddDiscountItem([FromBody] AddDiscountItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _discountService.AddDiscountItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove items from a discount</summary>
    [HttpDelete("items")]
    public async Task<IActionResult> DeleteDiscountItem([FromBody] DeleteDiscountItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _discountService.DeleteDiscountItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update items within a discount</summary>
    [HttpPut("items")]
    public async Task<IActionResult> UpdateDiscountItem([FromBody] UpdateDiscountItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _discountService.UpdateDiscountItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
