using ApiToolKit.Application.DTOs.Merchant.Account;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// accounts (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantAccountController : ControllerBase
{
    private readonly IMerchantAccountService _accountService;

    public MerchantAccountController(IMerchantAccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>Creates a Merchant Center test account.</summary>
    [HttpPost("accounts/{accountId}/createTestAccount")]
    public async Task<IActionResult> CreateTestAccountAsync([FromRoute] string accountId, [FromBody] CreateTestAccountRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountService.CreateTestAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a Merchant Center account with additional configuration.</summary>
    [HttpPost("accounts/createAndConfigure")]
    public async Task<IActionResult> CreateAndConfigureAccountAsync([FromBody] CreateAndConfigureAccountRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountService.CreateAndConfigureAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes the specified account regardless of its type: standalone, advanced account or sub-account.</summary>
    [HttpDelete("accounts/{accountId}")]
    public async Task<IActionResult> DeleteAccountAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] bool? force = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteAccountRequest { AccountKey = accountKey, AccountId = accountId, Force = force };
        var result = await _accountService.DeleteAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an account regardless of its type: standalone, advanced account or sub-account.</summary>
    [HttpPatch("accounts/{accountId}")]
    public async Task<IActionResult> UpdateAccountAsync([FromRoute] string accountId, [FromBody] UpdateAccountRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountService.UpdateAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves an account from your Merchant Center account.</summary>
    [HttpGet("accounts/{accountId}")]
    public async Task<IActionResult> GetAccountAsync([FromQuery] string accountKey, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAccountRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _accountService.GetAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Note: For the `accounts.list` method, quota and limits usage are charged for each user, and not for the Merchant Center ID or the advanced account ID.</summary>
    [HttpGet("accounts")]
    public async Task<IActionResult> ListAccountsAsync([FromQuery] string accountKey, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? filter = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAccountsRequest { AccountKey = accountKey, PageToken = pageToken, PageSize = pageSize, Filter = filter };
        var result = await _accountService.ListAccountsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List all sub-accounts for a given advanced account.</summary>
    [HttpGet("accounts/{accountId}/listSubaccounts")]
    public async Task<IActionResult> ListSubaccountsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListSubaccountsRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _accountService.ListSubaccountsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
