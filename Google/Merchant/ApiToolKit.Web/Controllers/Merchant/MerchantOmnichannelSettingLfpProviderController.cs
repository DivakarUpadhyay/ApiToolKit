using ApiToolKit.Application.DTOs.Merchant.OmnichannelSettingLfpProvider;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// omnichannelSettings / lfpProviders (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantOmnichannelSettingLfpProviderController : ControllerBase
{
    private readonly IMerchantOmnichannelSettingLfpProviderService _omnichannelSettingLfpProviderService;

    public MerchantOmnichannelSettingLfpProviderController(IMerchantOmnichannelSettingLfpProviderService omnichannelSettingLfpProviderService)
    {
        _omnichannelSettingLfpProviderService = omnichannelSettingLfpProviderService;
    }

    /// <summary>Find the LFP provider candidates in a given country.</summary>
    [HttpGet("accounts/{accountId}/omnichannelSettings/{omnichannelSettingId}/lfpProviders/find")]
    public async Task<IActionResult> FindLfpProviderAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string omnichannelSettingId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new FindLfpProviderRequest { AccountKey = accountKey, AccountId = accountId, OmnichannelSettingId = omnichannelSettingId, PageSize = pageSize, PageToken = pageToken };
        var result = await _omnichannelSettingLfpProviderService.FindLfpProviderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Link the specified merchant to a LFP provider for the specified country.</summary>
    [HttpPost("accounts/{accountId}/omnichannelSettings/{omnichannelSettingId}/lfpProviders/{lfpProviderId}/linkLfpProvider")]
    public async Task<IActionResult> LinkLfpProviderAsync([FromRoute] string accountId, [FromRoute] string omnichannelSettingId, [FromRoute] string lfpProviderId, [FromBody] LinkLfpProviderRequest request)
    {
        request.AccountId = accountId;
        request.OmnichannelSettingId = omnichannelSettingId;
        request.LfpProviderId = lfpProviderId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _omnichannelSettingLfpProviderService.LinkLfpProviderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
