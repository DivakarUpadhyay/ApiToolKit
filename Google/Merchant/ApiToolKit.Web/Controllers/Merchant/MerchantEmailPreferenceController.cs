using ApiToolKit.Application.DTOs.Merchant.EmailPreference;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// emailPreferences (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantEmailPreferenceController : ControllerBase
{
    private readonly IMerchantEmailPreferenceService _emailPreferenceService;

    public MerchantEmailPreferenceController(IMerchantEmailPreferenceService emailPreferenceService)
    {
        _emailPreferenceService = emailPreferenceService;
    }

    /// <summary>Returns the email preferences for a Merchant Center account user.</summary>
    [HttpGet("accounts/{accountId}/users/{userId}/emailPreferences")]
    public async Task<IActionResult> GetEmailPreferencesAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string userId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetEmailPreferencesRequest { AccountKey = accountKey, AccountId = accountId, UserId = userId };
        var result = await _emailPreferenceService.GetEmailPreferencesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates the email preferences for a Merchant Center account user.</summary>
    [HttpPatch("accounts/{accountId}/users/{userId}/emailPreferences")]
    public async Task<IActionResult> UpdateEmailPreferencesAsync([FromRoute] string accountId, [FromRoute] string userId, [FromBody] UpdateEmailPreferencesRequest request)
    {
        request.AccountId = accountId;
        request.UserId = userId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _emailPreferenceService.UpdateEmailPreferencesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
