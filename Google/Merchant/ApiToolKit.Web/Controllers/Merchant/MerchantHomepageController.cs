using ApiToolKit.Application.DTOs.Merchant.Homepage;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// homepage (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantHomepageController : ControllerBase
{
    private readonly IMerchantHomepageService _homepageService;

    public MerchantHomepageController(IMerchantHomepageService homepageService)
    {
        _homepageService = homepageService;
    }

    /// <summary>Unclaims a store's homepage.</summary>
    [HttpPost("accounts/{accountId}/homepage/unclaim")]
    public async Task<IActionResult> UnclaimHomepageAsync([FromRoute] string accountId, [FromBody] UnclaimHomepageRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _homepageService.UnclaimHomepageAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Claims a store's homepage.</summary>
    [HttpPost("accounts/{accountId}/homepage/claim")]
    public async Task<IActionResult> ClaimHomepageAsync([FromRoute] string accountId, [FromBody] ClaimHomepageRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _homepageService.ClaimHomepageAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a store's homepage.</summary>
    [HttpGet("accounts/{accountId}/homepage")]
    public async Task<IActionResult> GetHomepageAsync([FromQuery] string accountKey, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetHomepageRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _homepageService.GetHomepageAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates a store's homepage.</summary>
    [HttpPatch("accounts/{accountId}/homepage")]
    public async Task<IActionResult> UpdateHomepageAsync([FromRoute] string accountId, [FromBody] UpdateHomepageRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _homepageService.UpdateHomepageAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
