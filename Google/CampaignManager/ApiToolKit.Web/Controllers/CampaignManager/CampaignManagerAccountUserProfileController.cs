using ApiToolKit.Application.DTOs.CampaignManager.AccountUserProfile;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// accountUserProfiles (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerAccountUserProfileController : ControllerBase
{
    private readonly ICampaignManagerAccountUserProfileService _accountUserProfileService;

    public CampaignManagerAccountUserProfileController(ICampaignManagerAccountUserProfileService accountUserProfileService)
    {
        _accountUserProfileService = accountUserProfileService;
    }

    /// <summary>Inserts a new account user profile.</summary>
    [HttpPost("userprofiles/{userprofileId}/accountUserProfiles")]
    public async Task<IActionResult> InsertAccountUserProfileAsync([FromRoute] string userprofileId, [FromBody] InsertAccountUserProfileRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountUserProfileService.InsertAccountUserProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of account user profiles, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/accountUserProfiles")]
    public async Task<IActionResult> ListAccountUserProfilesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] int? maxResults = null, [FromQuery] List<string>? ids = null, [FromQuery] string? userRoleId = null, [FromQuery] string? pageToken = null, [FromQuery] string? subaccountId = null, [FromQuery] string? sortOrder = null, [FromQuery] string? sortField = null, [FromQuery] string? searchString = null, [FromQuery] bool? active = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAccountUserProfilesRequest { AccountKey = accountKey, UserprofileId = userprofileId, MaxResults = maxResults, Ids = ids, UserRoleId = userRoleId, PageToken = pageToken, SubaccountId = subaccountId, SortOrder = sortOrder, SortField = sortField, SearchString = searchString, Active = active };
        var result = await _accountUserProfileService.ListAccountUserProfilesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing account user profile.</summary>
    [HttpPut("userprofiles/{userprofileId}/accountUserProfiles")]
    public async Task<IActionResult> UpdateAccountUserProfileAsync([FromRoute] string userprofileId, [FromBody] UpdateAccountUserProfileRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountUserProfileService.UpdateAccountUserProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing account user profile.</summary>
    [HttpPatch("userprofiles/{userprofileId}/accountUserProfiles")]
    public async Task<IActionResult> PatchAccountUserProfileAsync([FromRoute] string userprofileId, [FromBody] PatchAccountUserProfileRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _accountUserProfileService.PatchAccountUserProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one account user profile by ID.</summary>
    [HttpGet("userprofiles/{profileId}/accountUserProfiles/{accountUserProfileId}")]
    public async Task<IActionResult> GetAccountUserProfileAsync([FromQuery] string accountKey, [FromRoute] string profileId, [FromRoute] string accountUserProfileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAccountUserProfileRequest { AccountKey = accountKey, ProfileId = profileId, AccountUserProfileId = accountUserProfileId };
        var result = await _accountUserProfileService.GetAccountUserProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
