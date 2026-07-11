using ApiToolKit.Application.DTOs.Shopee.Shop;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/shop")]
[Produces("application/json")]
public class ShopeeShopController : ControllerBase
{
    private readonly IShopeeShopService _shopService;

    public ShopeeShopController(IShopeeShopService shopService)
    {
        _shopService = shopService;
    }

    /// <summary>Get shop info by shop ID</summary>
    [HttpGet("{shopId}/info")]
    public async Task<IActionResult> GetShopInfo(long shopId)
    {
        var result = await _shopService.GetShopInfoAsync(shopId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get all shops under the partner</summary>
    [HttpGet("list")]
    public async Task<IActionResult> GetShopsByPartner([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _shopService.GetShopsByPartnerAsync(pageNo, pageSize);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get shop profile</summary>
    [HttpGet("{shopId}/profile")]
    public async Task<IActionResult> GetShopProfile(long shopId)
    {
        var result = await _shopService.GetShopProfileAsync(shopId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update shop profile</summary>
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateShopProfile([FromBody] UpdateShopProfileRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shopService.UpdateShopProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
