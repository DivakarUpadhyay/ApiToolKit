using ApiToolKit.Application.DTOs.Merchant.BusinessInfo;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// businessInfo (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantBusinessInfoController : ControllerBase
{
    private readonly IMerchantBusinessInfoService _businessInfoService;

    public MerchantBusinessInfoController(IMerchantBusinessInfoService businessInfoService)
    {
        _businessInfoService = businessInfoService;
    }

    /// <summary>Retrieves the business info of an account.</summary>
    [HttpGet("accounts/{accountId}/businessInfo")]
    public async Task<IActionResult> GetBusinessInfoAsync([FromQuery] string accountKey, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetBusinessInfoRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _businessInfoService.GetBusinessInfoAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates the business info of an account.</summary>
    [HttpPatch("accounts/{accountId}/businessInfo")]
    public async Task<IActionResult> UpdateBusinessInfoAsync([FromRoute] string accountId, [FromBody] UpdateBusinessInfoRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _businessInfoService.UpdateBusinessInfoAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
