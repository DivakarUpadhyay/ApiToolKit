using ApiToolKit.Application.DTOs.DisplayVideo.CustomList;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// customLists (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoCustomListController : ControllerBase
{
    private readonly IDisplayVideoCustomListService _customListService;

    public DisplayVideoCustomListController(IDisplayVideoCustomListService customListService)
    {
        _customListService = customListService;
    }

    /// <summary>Lists custom lists.</summary>
    [HttpGet("customLists")]
    public async Task<IActionResult> ListCustomListsAsync([FromQuery] string accountKey, [FromQuery] string? filter = null, [FromQuery] string? orderBy = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListCustomListsRequest { AccountKey = accountKey, Filter = filter, OrderBy = orderBy, PageSize = pageSize, PageToken = pageToken, AdvertiserId = advertiserId };
        var result = await _customListService.ListCustomListsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a custom list.</summary>
    [HttpGet("customLists/{customListId}")]
    public async Task<IActionResult> GetCustomListAsync([FromQuery] string accountKey, [FromRoute] string customListId, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetCustomListRequest { AccountKey = accountKey, CustomListId = customListId, AdvertiserId = advertiserId };
        var result = await _customListService.GetCustomListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
