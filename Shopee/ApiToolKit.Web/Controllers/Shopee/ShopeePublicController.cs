using ApiToolKit.Application.DTOs.Shopee.Public;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/public")]
[Produces("application/json")]
public class ShopeePublicController : ControllerBase
{
    private readonly IShopeePublicService _publicService;

    public ShopeePublicController(IShopeePublicService publicService)
    {
        _publicService = publicService;
    }

    /// <summary>Get list of shops authorized under this partner</summary>
    [HttpGet("shops")]
    public async Task<IActionResult> GetShopsByPartner([FromQuery] GetShopsByPartnerRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _publicService.GetShopsByPartnerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get product categories available for a specific country</summary>
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategoriesByCountry([FromQuery] GetCategoriesByCountryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _publicService.GetCategoriesByCountryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get available payment methods for a region</summary>
    [HttpGet("payment-methods")]
    public async Task<IActionResult> GetPaymentMethodList([FromQuery] GetPaymentMethodListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _publicService.GetPaymentMethodListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
