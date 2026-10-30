using ApiToolKit.Application.DTOs.Merchant.ProgramCheckoutSetting;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// programs / checkoutSettings (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantProgramCheckoutSettingController : ControllerBase
{
    private readonly IMerchantProgramCheckoutSettingService _programCheckoutSettingService;

    public MerchantProgramCheckoutSettingController(IMerchantProgramCheckoutSettingService programCheckoutSettingService)
    {
        _programCheckoutSettingService = programCheckoutSettingService;
    }

    /// <summary>Deletes `CheckoutSettings` and unenrolls merchant from `Checkout` program.</summary>
    [HttpDelete("accounts/{accountId}/programs/{programId}/checkoutSettings")]
    public async Task<IActionResult> DeleteCheckoutSettingsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string programId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteCheckoutSettingsRequest { AccountKey = accountKey, AccountId = accountId, ProgramId = programId };
        var result = await _programCheckoutSettingService.DeleteCheckoutSettingsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates `CheckoutSettings` for the given merchant.</summary>
    [HttpPost("accounts/{accountId}/programs/{programId}/checkoutSettings")]
    public async Task<IActionResult> CreateCheckoutSettingAsync([FromRoute] string accountId, [FromRoute] string programId, [FromBody] CreateCheckoutSettingRequest request)
    {
        request.AccountId = accountId;
        request.ProgramId = programId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _programCheckoutSettingService.CreateCheckoutSettingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates `CheckoutSettings` for the given merchant.</summary>
    [HttpPatch("accounts/{accountId}/programs/{programId}/checkoutSettings")]
    public async Task<IActionResult> UpdateCheckoutSettingsAsync([FromRoute] string accountId, [FromRoute] string programId, [FromBody] UpdateCheckoutSettingsRequest request)
    {
        request.AccountId = accountId;
        request.ProgramId = programId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _programCheckoutSettingService.UpdateCheckoutSettingsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets `CheckoutSettings` for the given merchant.</summary>
    [HttpGet("accounts/{accountId}/programs/{programId}/checkoutSettings")]
    public async Task<IActionResult> GetCheckoutSettingsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string programId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetCheckoutSettingsRequest { AccountKey = accountKey, AccountId = accountId, ProgramId = programId };
        var result = await _programCheckoutSettingService.GetCheckoutSettingsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
