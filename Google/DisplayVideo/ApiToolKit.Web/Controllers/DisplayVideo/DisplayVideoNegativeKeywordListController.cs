using ApiToolKit.Application.DTOs.DisplayVideo.NegativeKeywordList;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// negativeKeywordLists (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoNegativeKeywordListController : ControllerBase
{
    private readonly IDisplayVideoNegativeKeywordListService _negativeKeywordListService;

    public DisplayVideoNegativeKeywordListController(IDisplayVideoNegativeKeywordListService negativeKeywordListService)
    {
        _negativeKeywordListService = negativeKeywordListService;
    }

    /// <summary>Deletes a negative keyword list given an advertiser ID and a negative keyword list ID.</summary>
    [HttpDelete("advertisers/{advertiserId}/negativeKeywordLists/{negativeKeywordListId}")]
    public async Task<IActionResult> DeleteNegativeKeywordListAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string negativeKeywordListId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteNegativeKeywordListRequest { AccountKey = accountKey, AdvertiserId = advertiserId, NegativeKeywordListId = negativeKeywordListId };
        var result = await _negativeKeywordListService.DeleteNegativeKeywordListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists negative keyword lists based on a given advertiser id.</summary>
    [HttpGet("advertisers/{advertiserId}/negativeKeywordLists")]
    public async Task<IActionResult> ListNegativeKeywordListsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListNegativeKeywordListsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, PageSize = pageSize, PageToken = pageToken };
        var result = await _negativeKeywordListService.ListNegativeKeywordListsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates a negative keyword list.</summary>
    [HttpPatch("advertisers/{advertiserId}/negativeKeywordLists/{negativeKeywordListId}")]
    public async Task<IActionResult> UpdateNegativeKeywordListAsync([FromRoute] string advertiserId, [FromRoute] string negativeKeywordListId, [FromBody] UpdateNegativeKeywordListRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.NegativeKeywordListId = negativeKeywordListId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _negativeKeywordListService.UpdateNegativeKeywordListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a negative keyword list given an advertiser ID and a negative keyword list ID.</summary>
    [HttpGet("advertisers/{advertiserId}/negativeKeywordLists/{negativeKeywordListId}")]
    public async Task<IActionResult> GetNegativeKeywordListAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string negativeKeywordListId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetNegativeKeywordListRequest { AccountKey = accountKey, AdvertiserId = advertiserId, NegativeKeywordListId = negativeKeywordListId };
        var result = await _negativeKeywordListService.GetNegativeKeywordListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new negative keyword list.</summary>
    [HttpPost("advertisers/{advertiserId}/negativeKeywordLists")]
    public async Task<IActionResult> CreateNegativeKeywordListAsync([FromRoute] string advertiserId, [FromBody] CreateNegativeKeywordListRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _negativeKeywordListService.CreateNegativeKeywordListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
