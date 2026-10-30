using ApiToolKit.Application.DTOs.CampaignManager.ContentCategory;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// contentCategories (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerContentCategoryController : ControllerBase
{
    private readonly ICampaignManagerContentCategoryService _contentCategoryService;

    public CampaignManagerContentCategoryController(ICampaignManagerContentCategoryService contentCategoryService)
    {
        _contentCategoryService = contentCategoryService;
    }

    /// <summary>Deletes an existing content category.</summary>
    [HttpDelete("userprofiles/{userprofileId}/contentCategories/{contentCategoryId}")]
    public async Task<IActionResult> DeleteContentCategoryAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string contentCategoryId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteContentCategoryRequest { AccountKey = accountKey, UserprofileId = userprofileId, ContentCategoryId = contentCategoryId };
        var result = await _contentCategoryService.DeleteContentCategoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of content categories, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/contentCategories")]
    public async Task<IActionResult> ListContentCategoriesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] int? maxResults = null, [FromQuery] string? sortField = null, [FromQuery] string? pageToken = null, [FromQuery] string? searchString = null, [FromQuery] List<string>? ids = null, [FromQuery] string? sortOrder = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListContentCategoriesRequest { AccountKey = accountKey, UserprofileId = userprofileId, MaxResults = maxResults, SortField = sortField, PageToken = pageToken, SearchString = searchString, Ids = ids, SortOrder = sortOrder };
        var result = await _contentCategoryService.ListContentCategoriesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing content category.</summary>
    [HttpPut("userprofiles/{userprofileId}/contentCategories")]
    public async Task<IActionResult> UpdateContentCategoryAsync([FromRoute] string userprofileId, [FromBody] UpdateContentCategoryRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _contentCategoryService.UpdateContentCategoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one content category by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/contentCategories/{contentCategoryId}")]
    public async Task<IActionResult> GetContentCategoryAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string contentCategoryId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetContentCategoryRequest { AccountKey = accountKey, UserprofileId = userprofileId, ContentCategoryId = contentCategoryId };
        var result = await _contentCategoryService.GetContentCategoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new content category.</summary>
    [HttpPost("userprofiles/{userprofileId}/contentCategories")]
    public async Task<IActionResult> InsertContentCategoryAsync([FromRoute] string userprofileId, [FromBody] InsertContentCategoryRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _contentCategoryService.InsertContentCategoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing content category.</summary>
    [HttpPatch("userprofiles/{userprofileId}/contentCategories")]
    public async Task<IActionResult> PatchContentCategoryAsync([FromRoute] string userprofileId, [FromBody] PatchContentCategoryRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _contentCategoryService.PatchContentCategoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
