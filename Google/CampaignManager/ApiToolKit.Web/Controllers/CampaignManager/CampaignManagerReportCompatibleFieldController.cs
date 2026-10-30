using ApiToolKit.Application.DTOs.CampaignManager.ReportCompatibleField;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// reports / compatibleFields (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerReportCompatibleFieldController : ControllerBase
{
    private readonly ICampaignManagerReportCompatibleFieldService _reportCompatibleFieldService;

    public CampaignManagerReportCompatibleFieldController(ICampaignManagerReportCompatibleFieldService reportCompatibleFieldService)
    {
        _reportCompatibleFieldService = reportCompatibleFieldService;
    }

    /// <summary>Returns the fields that are compatible to be selected in the respective sections of a report criteria, given the fields already selected in the input report and user permissions.</summary>
    [HttpPost("userprofiles/{profileId}/reports/compatiblefields/query")]
    public async Task<IActionResult> QueryCompatibleFieldAsync([FromRoute] string profileId, [FromBody] QueryCompatibleFieldRequest request)
    {
        request.ProfileId = profileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reportCompatibleFieldService.QueryCompatibleFieldAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
