using ApiToolKit.Application.DTOs.CampaignManager.Subaccount;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// subaccounts (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerSubaccountController : ControllerBase
{
    private readonly ICampaignManagerSubaccountService _subaccountService;

    public CampaignManagerSubaccountController(ICampaignManagerSubaccountService subaccountService)
    {
        _subaccountService = subaccountService;
    }

    /// <summary>Updates an existing subaccount.</summary>
    [HttpPut("userprofiles/{userprofileId}/subaccounts")]
    public async Task<IActionResult> UpdateSubaccountAsync([FromRoute] string userprofileId, [FromBody] UpdateSubaccountRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _subaccountService.UpdateSubaccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing subaccount.</summary>
    [HttpPatch("userprofiles/{userprofileId}/subaccounts")]
    public async Task<IActionResult> PatchSubaccountAsync([FromRoute] string userprofileId, [FromBody] PatchSubaccountRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _subaccountService.PatchSubaccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new subaccount.</summary>
    [HttpPost("userprofiles/{userprofileId}/subaccounts")]
    public async Task<IActionResult> InsertSubaccountAsync([FromRoute] string userprofileId, [FromBody] InsertSubaccountRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _subaccountService.InsertSubaccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one subaccount by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/subaccounts/{subaccountId}")]
    public async Task<IActionResult> GetSubaccountAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string subaccountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetSubaccountRequest { AccountKey = accountKey, UserprofileId = userprofileId, SubaccountId = subaccountId };
        var result = await _subaccountService.GetSubaccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a list of subaccounts, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/subaccounts")]
    public async Task<IActionResult> ListSubaccountsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? searchString = null, [FromQuery] string? sortOrder = null, [FromQuery] List<string>? ids = null, [FromQuery] string? pageToken = null, [FromQuery] int? maxResults = null, [FromQuery] string? sortField = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListSubaccountsRequest { AccountKey = accountKey, UserprofileId = userprofileId, SearchString = searchString, SortOrder = sortOrder, Ids = ids, PageToken = pageToken, MaxResults = maxResults, SortField = sortField };
        var result = await _subaccountService.ListSubaccountsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
