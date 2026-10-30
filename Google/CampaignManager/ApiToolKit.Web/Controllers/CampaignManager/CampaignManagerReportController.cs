using ApiToolKit.Application.DTOs.CampaignManager.Report;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// reports (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerReportController : ControllerBase
{
    private readonly ICampaignManagerReportService _reportService;

    public CampaignManagerReportController(ICampaignManagerReportService reportService)
    {
        _reportService = reportService;
    }

    /// <summary>Runs a report.</summary>
    [HttpPost("userprofiles/{profileId}/reports/{reportId}/run")]
    public async Task<IActionResult> RunReportAsync([FromRoute] string profileId, [FromRoute] string reportId, [FromBody] RunReportRequest request)
    {
        request.ProfileId = profileId;
        request.ReportId = reportId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reportService.RunReportAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a report by its ID.</summary>
    [HttpDelete("userprofiles/{profileId}/reports/{reportId}")]
    public async Task<IActionResult> DeleteReportAsync([FromQuery] string accountKey, [FromRoute] string profileId, [FromRoute] string reportId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteReportRequest { AccountKey = accountKey, ProfileId = profileId, ReportId = reportId };
        var result = await _reportService.DeleteReportAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves list of reports.</summary>
    [HttpGet("userprofiles/{profileId}/reports")]
    public async Task<IActionResult> ListReportsAsync([FromQuery] string accountKey, [FromRoute] string profileId, [FromQuery] string? sortOrder = null, [FromQuery] int? maxResults = null, [FromQuery] string? pageToken = null, [FromQuery] string? scope = null, [FromQuery] string? sortField = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListReportsRequest { AccountKey = accountKey, ProfileId = profileId, SortOrder = sortOrder, MaxResults = maxResults, PageToken = pageToken, Scope = scope, SortField = sortField };
        var result = await _reportService.ListReportsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a report by its ID.</summary>
    [HttpGet("userprofiles/{profileId}/reports/{reportId}")]
    public async Task<IActionResult> GetReportAsync([FromQuery] string accountKey, [FromRoute] string profileId, [FromRoute] string reportId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetReportRequest { AccountKey = accountKey, ProfileId = profileId, ReportId = reportId };
        var result = await _reportService.GetReportAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates a report.</summary>
    [HttpPut("userprofiles/{profileId}/reports/{reportId}")]
    public async Task<IActionResult> UpdateReportAsync([FromRoute] string profileId, [FromRoute] string reportId, [FromBody] UpdateReportRequest request)
    {
        request.ProfileId = profileId;
        request.ReportId = reportId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reportService.UpdateReportAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a report.</summary>
    [HttpPost("userprofiles/{profileId}/reports")]
    public async Task<IActionResult> InsertReportAsync([FromRoute] string profileId, [FromBody] InsertReportRequest request)
    {
        request.ProfileId = profileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reportService.InsertReportAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
