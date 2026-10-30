using ApiToolKit.Application.DTOs.Merchant.LfpLfpSale;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// lfpSales (v1)
/// </summary>
[ApiController]
[Route("api/merchant/lfp")]
[Produces("application/json")]
public class MerchantLfpLfpSaleController : ControllerBase
{
    private readonly IMerchantLfpLfpSaleService _lfpLfpSaleService;

    public MerchantLfpLfpSaleController(IMerchantLfpLfpSaleService lfpLfpSaleService)
    {
        _lfpLfpSaleService = lfpLfpSaleService;
    }

    /// <summary>Inserts a `LfpSale` for the given merchant.</summary>
    [HttpPost("accounts/{accountId}/lfpSales/insert")]
    public async Task<IActionResult> InsertLfpSaleAsync([FromRoute] string accountId, [FromBody] InsertLfpSaleRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _lfpLfpSaleService.InsertLfpSaleAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
