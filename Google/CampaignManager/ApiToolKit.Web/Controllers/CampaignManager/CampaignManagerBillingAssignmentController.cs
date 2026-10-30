using ApiToolKit.Application.DTOs.CampaignManager.BillingAssignment;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// billingAssignments (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerBillingAssignmentController : ControllerBase
{
    private readonly ICampaignManagerBillingAssignmentService _billingAssignmentService;

    public CampaignManagerBillingAssignmentController(ICampaignManagerBillingAssignmentService billingAssignmentService)
    {
        _billingAssignmentService = billingAssignmentService;
    }

    /// <summary>Inserts a new billing assignment and returns the new assignment.</summary>
    [HttpPost("userprofiles/{userprofileId}/billingProfiles/{billingProfileId}/billingAssignments")]
    public async Task<IActionResult> InsertBillingAssignmentAsync([FromRoute] string userprofileId, [FromRoute] string billingProfileId, [FromBody] InsertBillingAssignmentRequest request)
    {
        request.UserprofileId = userprofileId;
        request.BillingProfileId = billingProfileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _billingAssignmentService.InsertBillingAssignmentAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of billing assignments.</summary>
    [HttpGet("userprofiles/{userprofileId}/billingProfiles/{billingProfileId}/billingAssignments")]
    public async Task<IActionResult> ListBillingAssignmentsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string billingProfileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListBillingAssignmentsRequest { AccountKey = accountKey, UserprofileId = userprofileId, BillingProfileId = billingProfileId };
        var result = await _billingAssignmentService.ListBillingAssignmentsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
