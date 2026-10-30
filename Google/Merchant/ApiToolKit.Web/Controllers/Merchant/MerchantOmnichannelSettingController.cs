using ApiToolKit.Application.DTOs.Merchant.OmnichannelSetting;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// omnichannelSettings (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantOmnichannelSettingController : ControllerBase
{
    private readonly IMerchantOmnichannelSettingService _omnichannelSettingService;

    public MerchantOmnichannelSettingController(IMerchantOmnichannelSettingService omnichannelSettingService)
    {
        _omnichannelSettingService = omnichannelSettingService;
    }

    /// <summary>List all the omnichannel settings for a given merchant.</summary>
    [HttpGet("accounts/{accountId}/omnichannelSettings")]
    public async Task<IActionResult> ListOmnichannelSettingsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListOmnichannelSettingsRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _omnichannelSettingService.ListOmnichannelSettingsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Requests inventory verification for a given merchant in a given country.</summary>
    [HttpPost("accounts/{accountId}/omnichannelSettings/{omnichannelSettingId}/requestInventoryVerification")]
    public async Task<IActionResult> RequestInventoryVerificationOmnichannelSettingAsync([FromRoute] string accountId, [FromRoute] string omnichannelSettingId, [FromBody] RequestInventoryVerificationOmnichannelSettingRequest request)
    {
        request.AccountId = accountId;
        request.OmnichannelSettingId = omnichannelSettingId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _omnichannelSettingService.RequestInventoryVerificationOmnichannelSettingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create the omnichannel settings for a given merchant.</summary>
    [HttpPost("accounts/{accountId}/omnichannelSettings")]
    public async Task<IActionResult> CreateOmnichannelSettingAsync([FromRoute] string accountId, [FromBody] CreateOmnichannelSettingRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _omnichannelSettingService.CreateOmnichannelSettingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the omnichannel settings for a given merchant.</summary>
    [HttpGet("accounts/{accountId}/omnichannelSettings/{omnichannelSettingId}")]
    public async Task<IActionResult> GetOmnichannelSettingAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string omnichannelSettingId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetOmnichannelSettingRequest { AccountKey = accountKey, AccountId = accountId, OmnichannelSettingId = omnichannelSettingId };
        var result = await _omnichannelSettingService.GetOmnichannelSettingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update the omnichannel setting for a given merchant in a given country.</summary>
    [HttpPatch("accounts/{accountId}/omnichannelSettings/{omnichannelSettingId}")]
    public async Task<IActionResult> UpdateOmnichannelSettingAsync([FromRoute] string accountId, [FromRoute] string omnichannelSettingId, [FromBody] UpdateOmnichannelSettingRequest request)
    {
        request.AccountId = accountId;
        request.OmnichannelSettingId = omnichannelSettingId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _omnichannelSettingService.UpdateOmnichannelSettingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
