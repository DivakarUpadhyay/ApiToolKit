using ApiToolKit.Application.DTOs.CampaignManager.TargetingTemplate;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// targetingTemplates (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerTargetingTemplateController : ControllerBase
{
    private readonly ICampaignManagerTargetingTemplateService _targetingTemplateService;

    public CampaignManagerTargetingTemplateController(ICampaignManagerTargetingTemplateService targetingTemplateService)
    {
        _targetingTemplateService = targetingTemplateService;
    }

    /// <summary>Inserts a new targeting template.</summary>
    [HttpPost("userprofiles/{userprofileId}/targetingTemplates")]
    public async Task<IActionResult> InsertTargetingTemplateAsync([FromRoute] string userprofileId, [FromBody] InsertTargetingTemplateRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _targetingTemplateService.InsertTargetingTemplateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing targeting template.</summary>
    [HttpPatch("userprofiles/{userprofileId}/targetingTemplates")]
    public async Task<IActionResult> UpdateTargetingTemplateAsync([FromRoute] string userprofileId, [FromBody] UpdateTargetingTemplateRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _targetingTemplateService.UpdateTargetingTemplateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one targeting template by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/targetingTemplates/{targetingTemplateId}")]
    public async Task<IActionResult> GetTargetingTemplateAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string targetingTemplateId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetTargetingTemplateRequest { AccountKey = accountKey, UserprofileId = userprofileId, TargetingTemplateId = targetingTemplateId };
        var result = await _targetingTemplateService.GetTargetingTemplateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing targeting template.</summary>
    [HttpPut("userprofiles/{userprofileId}/targetingTemplates")]
    public async Task<IActionResult> UpdateTargetingTemplateAltAsync([FromRoute] string userprofileId, [FromBody] UpdateTargetingTemplateAltRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _targetingTemplateService.UpdateTargetingTemplateAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of targeting templates, optionally filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/targetingTemplates")]
    public async Task<IActionResult> ListTargetingTemplatesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] List<string>? ids = null, [FromQuery] int? maxResults = null, [FromQuery] string? searchString = null, [FromQuery] string? sortField = null, [FromQuery] string? sortOrder = null, [FromQuery] string? advertiserId = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListTargetingTemplatesRequest { AccountKey = accountKey, UserprofileId = userprofileId, Ids = ids, MaxResults = maxResults, SearchString = searchString, SortField = sortField, SortOrder = sortOrder, AdvertiserId = advertiserId, PageToken = pageToken };
        var result = await _targetingTemplateService.ListTargetingTemplatesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
