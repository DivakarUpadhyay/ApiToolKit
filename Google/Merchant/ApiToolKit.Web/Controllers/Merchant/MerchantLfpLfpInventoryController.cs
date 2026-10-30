using ApiToolKit.Application.DTOs.Merchant.LfpLfpInventory;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// lfpInventories (v1)
/// </summary>
[ApiController]
[Route("api/merchant/lfp")]
[Produces("application/json")]
public class MerchantLfpLfpInventoryController : ControllerBase
{
    private readonly IMerchantLfpLfpInventoryService _lfpLfpInventoryService;

    public MerchantLfpLfpInventoryController(IMerchantLfpLfpInventoryService lfpLfpInventoryService)
    {
        _lfpLfpInventoryService = lfpLfpInventoryService;
    }

    /// <summary>Inserts a `LfpInventory` resource for the given target merchant account.</summary>
    [HttpPost("accounts/{accountId}/lfpInventories/insert")]
    public async Task<IActionResult> InsertLfpInventoryAsync([FromRoute] string accountId, [FromBody] InsertLfpInventoryRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _lfpLfpInventoryService.InsertLfpInventoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
