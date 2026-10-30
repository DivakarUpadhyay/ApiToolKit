using ApiToolKit.Application.DTOs.Merchant.GbpAccount;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// gbpAccounts (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantGbpAccountController : ControllerBase
{
    private readonly IMerchantGbpAccountService _gbpAccountService;

    public MerchantGbpAccountController(IMerchantGbpAccountService gbpAccountService)
    {
        _gbpAccountService = gbpAccountService;
    }

    /// <summary>List the GBP accounts for a given merchant.</summary>
    [HttpGet("accounts/{accountId}/gbpAccounts")]
    public async Task<IActionResult> ListGbpAccountsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListGbpAccountsRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _gbpAccountService.ListGbpAccountsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Link the specified merchant to a GBP account for all countries.</summary>
    [HttpPost("accounts/{accountId}/gbpAccounts/linkGbpAccount")]
    public async Task<IActionResult> LinkGbpAccountAsync([FromRoute] string accountId, [FromBody] LinkGbpAccountRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _gbpAccountService.LinkGbpAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
