using ApiToolKit.Application.DTOs.DisplayVideo.NegativeKeywordListNegativeKeyword;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// negativeKeywordLists / negativeKeywords (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoNegativeKeywordListNegativeKeywordController : ControllerBase
{
    private readonly IDisplayVideoNegativeKeywordListNegativeKeywordService _negativeKeywordListNegativeKeywordService;

    public DisplayVideoNegativeKeywordListNegativeKeywordController(IDisplayVideoNegativeKeywordListNegativeKeywordService negativeKeywordListNegativeKeywordService)
    {
        _negativeKeywordListNegativeKeywordService = negativeKeywordListNegativeKeywordService;
    }

    /// <summary>Bulk edits negative keywords in a single negative keyword list.</summary>
    [HttpPost("advertisers/{advertiserId}/negativeKeywordLists/{negativeKeywordListId}/negativeKeywords/bulkEdit")]
    public async Task<IActionResult> BulkEditNegativeKeywordAsync([FromRoute] string advertiserId, [FromRoute] string negativeKeywordListId, [FromBody] BulkEditNegativeKeywordRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.NegativeKeywordListId = negativeKeywordListId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _negativeKeywordListNegativeKeywordService.BulkEditNegativeKeywordAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a negative keyword from a negative keyword list.</summary>
    [HttpDelete("advertisers/{advertiserId}/negativeKeywordLists/{negativeKeywordListId}/negativeKeywords/{negativeKeywordId}")]
    public async Task<IActionResult> DeleteNegativeKeywordAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string negativeKeywordListId, [FromRoute] string negativeKeywordId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteNegativeKeywordRequest { AccountKey = accountKey, AdvertiserId = advertiserId, NegativeKeywordListId = negativeKeywordListId, NegativeKeywordId = negativeKeywordId };
        var result = await _negativeKeywordListNegativeKeywordService.DeleteNegativeKeywordAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists negative keywords in a negative keyword list.</summary>
    [HttpGet("advertisers/{advertiserId}/negativeKeywordLists/{negativeKeywordListId}/negativeKeywords")]
    public async Task<IActionResult> ListNegativeKeywordsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string negativeKeywordListId, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null, [FromQuery] string? filter = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListNegativeKeywordsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, NegativeKeywordListId = negativeKeywordListId, PageToken = pageToken, PageSize = pageSize, OrderBy = orderBy, Filter = filter };
        var result = await _negativeKeywordListNegativeKeywordService.ListNegativeKeywordsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Replaces all negative keywords in a single negative keyword list.</summary>
    [HttpPost("advertisers/{advertiserId}/negativeKeywordLists/{negativeKeywordListId}/negativeKeywords/replace")]
    public async Task<IActionResult> ReplaceNegativeKeywordAsync([FromRoute] string advertiserId, [FromRoute] string negativeKeywordListId, [FromBody] ReplaceNegativeKeywordRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.NegativeKeywordListId = negativeKeywordListId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _negativeKeywordListNegativeKeywordService.ReplaceNegativeKeywordAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a negative keyword in a negative keyword list.</summary>
    [HttpPost("advertisers/{advertiserId}/negativeKeywordLists/{negativeKeywordListId}/negativeKeywords")]
    public async Task<IActionResult> CreateNegativeKeywordAsync([FromRoute] string advertiserId, [FromRoute] string negativeKeywordListId, [FromBody] CreateNegativeKeywordRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.NegativeKeywordListId = negativeKeywordListId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _negativeKeywordListNegativeKeywordService.CreateNegativeKeywordAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
