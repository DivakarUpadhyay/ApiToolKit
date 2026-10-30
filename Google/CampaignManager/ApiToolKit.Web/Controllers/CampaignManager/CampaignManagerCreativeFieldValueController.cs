using ApiToolKit.Application.DTOs.CampaignManager.CreativeFieldValue;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// creativeFieldValues (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerCreativeFieldValueController : ControllerBase
{
    private readonly ICampaignManagerCreativeFieldValueService _creativeFieldValueService;

    public CampaignManagerCreativeFieldValueController(ICampaignManagerCreativeFieldValueService creativeFieldValueService)
    {
        _creativeFieldValueService = creativeFieldValueService;
    }

    /// <summary>Gets one creative field value by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/creativeFields/{creativeFieldId}/creativeFieldValues/{creativeFieldValueId}")]
    public async Task<IActionResult> GetCreativeFieldValueAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string creativeFieldId, [FromRoute] string creativeFieldValueId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetCreativeFieldValueRequest { AccountKey = accountKey, UserprofileId = userprofileId, CreativeFieldId = creativeFieldId, CreativeFieldValueId = creativeFieldValueId };
        var result = await _creativeFieldValueService.GetCreativeFieldValueAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing creative field value.</summary>
    [HttpPatch("userprofiles/{userprofileId}/creativeFields/{creativeFieldId}/creativeFieldValues")]
    public async Task<IActionResult> UpdateCreativeFieldValueAsync([FromRoute] string userprofileId, [FromRoute] string creativeFieldId, [FromBody] UpdateCreativeFieldValueRequest request)
    {
        request.UserprofileId = userprofileId;
        request.CreativeFieldId = creativeFieldId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeFieldValueService.UpdateCreativeFieldValueAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new creative field value.</summary>
    [HttpPost("userprofiles/{userprofileId}/creativeFields/{creativeFieldId}/creativeFieldValues")]
    public async Task<IActionResult> InsertCreativeFieldValueAsync([FromRoute] string userprofileId, [FromRoute] string creativeFieldId, [FromBody] InsertCreativeFieldValueRequest request)
    {
        request.UserprofileId = userprofileId;
        request.CreativeFieldId = creativeFieldId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeFieldValueService.InsertCreativeFieldValueAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing creative field value.</summary>
    [HttpPut("userprofiles/{userprofileId}/creativeFields/{creativeFieldId}/creativeFieldValues")]
    public async Task<IActionResult> UpdateCreativeFieldValueAltAsync([FromRoute] string userprofileId, [FromRoute] string creativeFieldId, [FromBody] UpdateCreativeFieldValueAltRequest request)
    {
        request.UserprofileId = userprofileId;
        request.CreativeFieldId = creativeFieldId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _creativeFieldValueService.UpdateCreativeFieldValueAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of creative field values, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/creativeFields/{creativeFieldId}/creativeFieldValues")]
    public async Task<IActionResult> ListCreativeFieldValuesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string creativeFieldId, [FromQuery] string? sortOrder = null, [FromQuery] string? searchString = null, [FromQuery] string? pageToken = null, [FromQuery] int? maxResults = null, [FromQuery] List<string>? ids = null, [FromQuery] string? sortField = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListCreativeFieldValuesRequest { AccountKey = accountKey, UserprofileId = userprofileId, CreativeFieldId = creativeFieldId, SortOrder = sortOrder, SearchString = searchString, PageToken = pageToken, MaxResults = maxResults, Ids = ids, SortField = sortField };
        var result = await _creativeFieldValueService.ListCreativeFieldValuesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an existing creative field value.</summary>
    [HttpDelete("userprofiles/{userprofileId}/creativeFields/{creativeFieldId}/creativeFieldValues/{creativeFieldValueId}")]
    public async Task<IActionResult> DeleteCreativeFieldValueAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string creativeFieldId, [FromRoute] string creativeFieldValueId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteCreativeFieldValueRequest { AccountKey = accountKey, UserprofileId = userprofileId, CreativeFieldId = creativeFieldId, CreativeFieldValueId = creativeFieldValueId };
        var result = await _creativeFieldValueService.DeleteCreativeFieldValueAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
