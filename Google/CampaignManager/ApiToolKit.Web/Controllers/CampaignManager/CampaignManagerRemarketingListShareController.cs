using ApiToolKit.Application.DTOs.CampaignManager.RemarketingListShare;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// remarketingListShares (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerRemarketingListShareController : ControllerBase
{
    private readonly ICampaignManagerRemarketingListShareService _remarketingListShareService;

    public CampaignManagerRemarketingListShareController(ICampaignManagerRemarketingListShareService remarketingListShareService)
    {
        _remarketingListShareService = remarketingListShareService;
    }

    /// <summary>Updates an existing remarketing list share.</summary>
    [HttpPut("userprofiles/{userprofileId}/remarketingListShares")]
    public async Task<IActionResult> UpdateRemarketingListShareAsync([FromRoute] string userprofileId, [FromBody] UpdateRemarketingListShareRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _remarketingListShareService.UpdateRemarketingListShareAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing remarketing list share.</summary>
    [HttpPatch("userprofiles/{userprofileId}/remarketingListShares")]
    public async Task<IActionResult> PatchRemarketingListShareAsync([FromRoute] string userprofileId, [FromBody] PatchRemarketingListShareRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _remarketingListShareService.PatchRemarketingListShareAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one remarketing list share by remarketing list ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/remarketingListShares/{remarketingListShareId}")]
    public async Task<IActionResult> GetRemarketingListShareAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string remarketingListShareId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetRemarketingListShareRequest { AccountKey = accountKey, UserprofileId = userprofileId, RemarketingListShareId = remarketingListShareId };
        var result = await _remarketingListShareService.GetRemarketingListShareAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
