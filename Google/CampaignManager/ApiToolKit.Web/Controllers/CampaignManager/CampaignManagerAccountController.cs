using ApiToolKit.Application.DTOs.CampaignManager.Account;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// accounts (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerAccountController : ControllerBase
{
    private readonly ICampaignManagerAccountService _accountService;

    public CampaignManagerAccountController(ICampaignManagerAccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>Gets one account by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/accounts/{accountId}")]
    public async Task<IActionResult> GetAccountAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAccountRequest { AccountKey = accountKey, UserprofileId = userprofileId, AccountId = accountId };
        var result = await _accountService.GetAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing account.</summary>
    [HttpPut("userprofiles/{userprofileId}/accounts")]
    public async Task<IActionResult> UpdateAccountAsync([FromRoute] string userprofileId, [FromBody] UpdateAccountRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountService.UpdateAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing account.</summary>
    [HttpPatch("userprofiles/{userprofileId}/accounts")]
    public async Task<IActionResult> PatchAccountAsync([FromRoute] string userprofileId, [FromBody] PatchAccountRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountService.PatchAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves the list of accounts, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/accounts")]
    public async Task<IActionResult> ListAccountsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? pageToken = null, [FromQuery] int? maxResults = null, [FromQuery] string? sortOrder = null, [FromQuery] bool? active = null, [FromQuery] List<string>? ids = null, [FromQuery] string? searchString = null, [FromQuery] string? sortField = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAccountsRequest { AccountKey = accountKey, UserprofileId = userprofileId, PageToken = pageToken, MaxResults = maxResults, SortOrder = sortOrder, Active = active, Ids = ids, SearchString = searchString, SortField = sortField };
        var result = await _accountService.ListAccountsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
