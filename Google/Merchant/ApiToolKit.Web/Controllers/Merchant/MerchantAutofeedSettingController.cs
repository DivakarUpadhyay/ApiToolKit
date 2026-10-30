using ApiToolKit.Application.DTOs.Merchant.AutofeedSetting;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// autofeedSettings (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantAutofeedSettingController : ControllerBase
{
    private readonly IMerchantAutofeedSettingService _autofeedSettingService;

    public MerchantAutofeedSettingController(IMerchantAutofeedSettingService autofeedSettingService)
    {
        _autofeedSettingService = autofeedSettingService;
    }

    /// <summary>Retrieves the autofeed settings of an account.</summary>
    [HttpGet("accounts/{accountId}/autofeedSettings")]
    public async Task<IActionResult> GetAutofeedSettingsAsync([FromQuery] string accountKey, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAutofeedSettingsRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _autofeedSettingService.GetAutofeedSettingsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates the autofeed settings of an account.</summary>
    [HttpPatch("accounts/{accountId}/autofeedSettings")]
    public async Task<IActionResult> UpdateAutofeedSettingsAsync([FromRoute] string accountId, [FromBody] UpdateAutofeedSettingsRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _autofeedSettingService.UpdateAutofeedSettingsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
