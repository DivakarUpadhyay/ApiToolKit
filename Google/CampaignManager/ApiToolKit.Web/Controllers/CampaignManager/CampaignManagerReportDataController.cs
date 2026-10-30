using ApiToolKit.Application.DTOs.CampaignManager.ReportData;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// reportData (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerReportDataController : ControllerBase
{
    private readonly ICampaignManagerReportDataService _reportDataService;

    public CampaignManagerReportDataController(ICampaignManagerReportDataService reportDataService)
    {
        _reportDataService = reportDataService;
    }

    /// <summary>Executes an ad-hoc query and returns structured JSON payload data.</summary>
    [HttpPost("userprofiles/{profileId}/reportdata/query")]
    public async Task<IActionResult> QueryReportDataAsync([FromRoute] string profileId, [FromBody] QueryReportDataRequest request)
    {
        request.ProfileId = profileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reportDataService.QueryReportDataAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
