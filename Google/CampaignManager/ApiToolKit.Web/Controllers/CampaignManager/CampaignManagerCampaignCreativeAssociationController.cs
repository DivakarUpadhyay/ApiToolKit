using ApiToolKit.Application.DTOs.CampaignManager.CampaignCreativeAssociation;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// campaignCreativeAssociations (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerCampaignCreativeAssociationController : ControllerBase
{
    private readonly ICampaignManagerCampaignCreativeAssociationService _campaignCreativeAssociationService;

    public CampaignManagerCampaignCreativeAssociationController(ICampaignManagerCampaignCreativeAssociationService campaignCreativeAssociationService)
    {
        _campaignCreativeAssociationService = campaignCreativeAssociationService;
    }

    /// <summary>Associates a creative with the specified campaign.</summary>
    [HttpPost("userprofiles/{userprofileId}/campaigns/{campaignId}/campaignCreativeAssociations")]
    public async Task<IActionResult> InsertCampaignCreativeAssociationAsync([FromRoute] string userprofileId, [FromRoute] string campaignId, [FromBody] InsertCampaignCreativeAssociationRequest request)
    {
        request.UserprofileId = userprofileId;
        request.CampaignId = campaignId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _campaignCreativeAssociationService.InsertCampaignCreativeAssociationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves the list of creative IDs associated with the specified campaign.</summary>
    [HttpGet("userprofiles/{userprofileId}/campaigns/{campaignId}/campaignCreativeAssociations")]
    public async Task<IActionResult> ListCampaignCreativeAssociationsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string campaignId, [FromQuery] string? pageToken = null, [FromQuery] string? sortOrder = null, [FromQuery] int? maxResults = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListCampaignCreativeAssociationsRequest { AccountKey = accountKey, UserprofileId = userprofileId, CampaignId = campaignId, PageToken = pageToken, SortOrder = sortOrder, MaxResults = maxResults };
        var result = await _campaignCreativeAssociationService.ListCampaignCreativeAssociationsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
