using ApiToolKit.Application.DTOs.CampaignManager.File;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// files (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerFileController : ControllerBase
{
    private readonly ICampaignManagerFileService _fileService;

    public CampaignManagerFileController(ICampaignManagerFileService fileService)
    {
        _fileService = fileService;
    }

    /// <summary>Lists files for a user profile.</summary>
    [HttpGet("userprofiles/{profileId}/files")]
    public async Task<IActionResult> ListFilesAsync([FromQuery] string accountKey, [FromRoute] string profileId, [FromQuery] string? sortOrder = null, [FromQuery] string? pageToken = null, [FromQuery] int? maxResults = null, [FromQuery] string? scope = null, [FromQuery] string? sortField = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListFilesRequest { AccountKey = accountKey, ProfileId = profileId, SortOrder = sortOrder, PageToken = pageToken, MaxResults = maxResults, Scope = scope, SortField = sortField };
        var result = await _fileService.ListFilesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a report file by its report ID and file ID.</summary>
    [HttpGet("reports/{reportId}/files/{fileId}")]
    public async Task<IActionResult> GetFileAsync([FromQuery] string accountKey, [FromRoute] string reportId, [FromRoute] string fileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetFileRequest { AccountKey = accountKey, ReportId = reportId, FileId = fileId };
        var result = await _fileService.GetFileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Download the file contents of: Retrieves a report file by its report ID and file ID.</summary>
    [HttpGet("reports/{reportId}/files/{fileId}/download")]
    public async Task<IActionResult> GetFileContentAsync([FromQuery] string accountKey, [FromRoute] string reportId, [FromRoute] string fileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetFileContentRequest { AccountKey = accountKey, ReportId = reportId, FileId = fileId };
        var result = await _fileService.GetFileContentAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
