using ApiToolKit.Application.DTOs.CampaignManager.UserProfile;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// userProfiles (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerUserProfileController : ControllerBase
{
    private readonly ICampaignManagerUserProfileService _userProfileService;

    public CampaignManagerUserProfileController(ICampaignManagerUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    /// <summary>Retrieves list of user profiles for a user.</summary>
    [HttpGet("userprofiles")]
    public async Task<IActionResult> ListUserProfilesAsync([FromQuery] string accountKey)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListUserProfilesRequest { AccountKey = accountKey };
        var result = await _userProfileService.ListUserProfilesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one user profile by ID.</summary>
    [HttpGet("userprofiles/{profileId}")]
    public async Task<IActionResult> GetUserProfileAsync([FromQuery] string accountKey, [FromRoute] string profileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetUserProfileRequest { AccountKey = accountKey, ProfileId = profileId };
        var result = await _userProfileService.GetUserProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
