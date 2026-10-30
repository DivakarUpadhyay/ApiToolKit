using ApiToolKit.Application.DTOs.CampaignManager.ChangeLog;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// changeLogs (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerChangeLogController : ControllerBase
{
    private readonly ICampaignManagerChangeLogService _changeLogService;

    public CampaignManagerChangeLogController(ICampaignManagerChangeLogService changeLogService)
    {
        _changeLogService = changeLogService;
    }

    /// <summary>Gets one change log by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/changeLogs/{changeLogId}")]
    public async Task<IActionResult> GetChangeLogAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string changeLogId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetChangeLogRequest { AccountKey = accountKey, UserprofileId = userprofileId, ChangeLogId = changeLogId };
        var result = await _changeLogService.GetChangeLogAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of change logs.</summary>
    [HttpGet("userprofiles/{userprofileId}/changeLogs")]
    public async Task<IActionResult> ListChangeLogsAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? maxChangeTime = null, [FromQuery] List<string>? ids = null, [FromQuery] List<string>? objectIds = null, [FromQuery] string? minChangeTime = null, [FromQuery] List<string>? userProfileIds = null, [FromQuery] int? maxResults = null, [FromQuery] string? action = null, [FromQuery] string? pageToken = null, [FromQuery] string? searchString = null, [FromQuery] string? objectType = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListChangeLogsRequest { AccountKey = accountKey, UserprofileId = userprofileId, MaxChangeTime = maxChangeTime, Ids = ids, ObjectIds = objectIds, MinChangeTime = minChangeTime, UserProfileIds = userProfileIds, MaxResults = maxResults, Action = action, PageToken = pageToken, SearchString = searchString, ObjectType = objectType };
        var result = await _changeLogService.ListChangeLogsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
