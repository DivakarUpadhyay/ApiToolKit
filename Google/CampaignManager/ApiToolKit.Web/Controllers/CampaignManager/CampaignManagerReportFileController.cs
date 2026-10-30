using ApiToolKit.Application.DTOs.CampaignManager.ReportFile;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// reports / files (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerReportFileController : ControllerBase
{
    private readonly ICampaignManagerReportFileService _reportFileService;

    public CampaignManagerReportFileController(ICampaignManagerReportFileService reportFileService)
    {
        _reportFileService = reportFileService;
    }

    /// <summary>Lists files for a report.</summary>
    [HttpGet("userprofiles/{profileId}/reports/{reportId}/files")]
    public async Task<IActionResult> ListFilesAsync([FromQuery] string accountKey, [FromRoute] string profileId, [FromRoute] string reportId, [FromQuery] int? maxResults = null, [FromQuery] string? sortField = null, [FromQuery] string? sortOrder = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListFilesRequest { AccountKey = accountKey, ProfileId = profileId, ReportId = reportId, MaxResults = maxResults, SortField = sortField, SortOrder = sortOrder, PageToken = pageToken };
        var result = await _reportFileService.ListFilesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a report file by its report ID and file ID.</summary>
    [HttpGet("userprofiles/{profileId}/reports/{reportId}/files/{fileId}")]
    public async Task<IActionResult> GetFileAsync([FromQuery] string accountKey, [FromRoute] string profileId, [FromRoute] string reportId, [FromRoute] string fileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetFileRequest { AccountKey = accountKey, ProfileId = profileId, ReportId = reportId, FileId = fileId };
        var result = await _reportFileService.GetFileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Download the file contents of: Retrieves a report file by its report ID and file ID.</summary>
    [HttpGet("userprofiles/{profileId}/reports/{reportId}/files/{fileId}/download")]
    public async Task<IActionResult> GetFileContentAsync([FromQuery] string accountKey, [FromRoute] string profileId, [FromRoute] string reportId, [FromRoute] string fileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetFileContentRequest { AccountKey = accountKey, ProfileId = profileId, ReportId = reportId, FileId = fileId };
        var result = await _reportFileService.GetFileContentAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
