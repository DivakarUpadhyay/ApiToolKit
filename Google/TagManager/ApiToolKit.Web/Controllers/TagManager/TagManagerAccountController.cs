using ApiToolKit.Application.DTOs.TagManager.Account;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Tag Manager accounts.
/// </summary>
[ApiController]
[Route("api/tagmanager/accounts")]
[Produces("application/json")]
public class TagManagerAccountController : ControllerBase
{
    private readonly ITagManagerAccountService _accountService;

    public TagManagerAccountController(ITagManagerAccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>List Tag Manager accounts.</summary>
    [HttpGet]
    public async Task<IActionResult> ListAccountsAsync([FromQuery] string accountKey, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAccountsRequest { AccountKey = accountKey, PageToken = pageToken };
        var result = await _accountService.ListAccountsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one Tag Manager account.</summary>
    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetAccountAsync([FromQuery] string accountKey, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAccountRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _accountService.GetAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a Tag Manager account.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateAccountAsync([FromBody] UpdateAccountRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountService.UpdateAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
