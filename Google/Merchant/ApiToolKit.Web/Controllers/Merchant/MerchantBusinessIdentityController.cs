using ApiToolKit.Application.DTOs.Merchant.BusinessIdentity;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// businessIdentity (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantBusinessIdentityController : ControllerBase
{
    private readonly IMerchantBusinessIdentityService _businessIdentityService;

    public MerchantBusinessIdentityController(IMerchantBusinessIdentityService businessIdentityService)
    {
        _businessIdentityService = businessIdentityService;
    }

    /// <summary>Retrieves the business identity of an account.</summary>
    [HttpGet("accounts/{accountId}/businessIdentity")]
    public async Task<IActionResult> GetBusinessIdentityAsync([FromQuery] string accountKey, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetBusinessIdentityRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _businessIdentityService.GetBusinessIdentityAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates the business identity of an account.</summary>
    [HttpPatch("accounts/{accountId}/businessIdentity")]
    public async Task<IActionResult> UpdateBusinessIdentityAsync([FromRoute] string accountId, [FromBody] UpdateBusinessIdentityRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _businessIdentityService.UpdateBusinessIdentityAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
