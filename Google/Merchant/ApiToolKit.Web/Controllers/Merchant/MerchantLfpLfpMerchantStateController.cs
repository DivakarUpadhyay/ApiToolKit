using ApiToolKit.Application.DTOs.Merchant.LfpLfpMerchantState;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// lfpMerchantStates (v1)
/// </summary>
[ApiController]
[Route("api/merchant/lfp")]
[Produces("application/json")]
public class MerchantLfpLfpMerchantStateController : ControllerBase
{
    private readonly IMerchantLfpLfpMerchantStateService _lfpLfpMerchantStateService;

    public MerchantLfpLfpMerchantStateController(IMerchantLfpLfpMerchantStateService lfpLfpMerchantStateService)
    {
        _lfpLfpMerchantStateService = lfpLfpMerchantStateService;
    }

    /// <summary>Gets the LFP state of a merchant</summary>
    [HttpGet("accounts/{accountId}/lfpMerchantStates/{lfpMerchantStateId}")]
    public async Task<IActionResult> GetLfpMerchantStateAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string lfpMerchantStateId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetLfpMerchantStateRequest { AccountKey = accountKey, AccountId = accountId, LfpMerchantStateId = lfpMerchantStateId };
        var result = await _lfpLfpMerchantStateService.GetLfpMerchantStateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
