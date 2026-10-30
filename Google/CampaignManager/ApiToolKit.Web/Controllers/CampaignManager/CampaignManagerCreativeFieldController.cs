using ApiToolKit.Application.DTOs.CampaignManager.CreativeField;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// creativeFields (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerCreativeFieldController : ControllerBase
{
    private readonly ICampaignManagerCreativeFieldService _creativeFieldService;

    public CampaignManagerCreativeFieldController(ICampaignManagerCreativeFieldService creativeFieldService)
    {
        _creativeFieldService = creativeFieldService;
    }

    /// <summary>Updates an existing creative field.</summary>
    [HttpPatch("userprofiles/{userprofileId}/creativeFields")]
    public async Task<IActionResult> UpdateCreativeFieldAsync([FromRoute] string userprofileId, [FromBody] UpdateCreativeFieldRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeFieldService.UpdateCreativeFieldAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one creative field by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/creativeFields/{creativeFieldId}")]
    public async Task<IActionResult> GetCreativeFieldAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string creativeFieldId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetCreativeFieldRequest { AccountKey = accountKey, UserprofileId = userprofileId, CreativeFieldId = creativeFieldId };
        var result = await _creativeFieldService.GetCreativeFieldAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new creative field.</summary>
    [HttpPost("userprofiles/{userprofileId}/creativeFields")]
    public async Task<IActionResult> InsertCreativeFieldAsync([FromRoute] string userprofileId, [FromBody] InsertCreativeFieldRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeFieldService.InsertCreativeFieldAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing creative field.</summary>
    [HttpPut("userprofiles/{userprofileId}/creativeFields")]
    public async Task<IActionResult> UpdateCreativeFieldAltAsync([FromRoute] string userprofileId, [FromBody] UpdateCreativeFieldAltRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeFieldService.UpdateCreativeFieldAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an existing creative field.</summary>
    [HttpDelete("userprofiles/{userprofileId}/creativeFields/{creativeFieldId}")]
    public async Task<IActionResult> DeleteCreativeFieldAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string creativeFieldId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteCreativeFieldRequest { AccountKey = accountKey, UserprofileId = userprofileId, CreativeFieldId = creativeFieldId };
        var result = await _creativeFieldService.DeleteCreativeFieldAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of creative fields, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/creativeFields")]
    public async Task<IActionResult> ListCreativeFieldsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? sortOrder = null, [FromQuery] List<string>? ids = null, [FromQuery] string? searchString = null, [FromQuery] string? pageToken = null, [FromQuery] int? maxResults = null, [FromQuery] List<string>? advertiserIds = null, [FromQuery] string? sortField = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListCreativeFieldsRequest { AccountKey = accountKey, UserprofileId = userprofileId, SortOrder = sortOrder, Ids = ids, SearchString = searchString, PageToken = pageToken, MaxResults = maxResults, AdvertiserIds = advertiserIds, SortField = sortField };
        var result = await _creativeFieldService.ListCreativeFieldsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
