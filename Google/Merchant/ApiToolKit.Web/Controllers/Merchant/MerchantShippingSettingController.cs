using ApiToolKit.Application.DTOs.Merchant.ShippingSetting;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// shippingSettings (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantShippingSettingController : ControllerBase
{
    private readonly IMerchantShippingSettingService _shippingSettingService;

    public MerchantShippingSettingController(IMerchantShippingSettingService shippingSettingService)
    {
        _shippingSettingService = shippingSettingService;
    }

    /// <summary>Replace the shipping setting of a business with the request shipping setting.</summary>
    [HttpPost("accounts/{accountId}/shippingSettings/insert")]
    public async Task<IActionResult> InsertShippingSettingAsync([FromRoute] string accountId, [FromBody] InsertShippingSettingRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _shippingSettingService.InsertShippingSettingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieve shipping setting information.</summary>
    [HttpGet("accounts/{accountId}/shippingSettings")]
    public async Task<IActionResult> GetShippingSettingsAsync([FromQuery] string accountKey, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetShippingSettingsRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _shippingSettingService.GetShippingSettingsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
