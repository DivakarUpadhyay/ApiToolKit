using ApiToolKit.Application.DTOs.Shopee.Merchant;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/merchant")]
[Produces("application/json")]
public class ShopeeMerchantController : ControllerBase
{
    private readonly IShopeeMerchantService _merchantService;

    public ShopeeMerchantController(IShopeeMerchantService merchantService)
    {
        _merchantService = merchantService;
    }

    /// <summary>Get merchant account information</summary>
    [HttpGet("{merchantId}/info")]
    public async Task<IActionResult> GetMerchantInfo(long merchantId)
    {
        var result = await _merchantService.GetMerchantInfoAsync(new GetMerchantInfoRequest { MerchantId = merchantId });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of shops under a merchant account</summary>
    [HttpGet("{merchantId}/shops")]
    public async Task<IActionResult> GetShopsByMerchant(long merchantId, [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _merchantService.GetShopsByMerchantAsync(new GetShopsByMerchantRequest { MerchantId = merchantId, PageNo = pageNo, PageSize = pageSize });
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
