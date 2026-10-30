using ApiToolKit.Application.DTOs.CampaignManager.DimensionValue;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// dimensionValues (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerDimensionValueController : ControllerBase
{
    private readonly ICampaignManagerDimensionValueService _dimensionValueService;

    public CampaignManagerDimensionValueController(ICampaignManagerDimensionValueService dimensionValueService)
    {
        _dimensionValueService = dimensionValueService;
    }

    /// <summary>Retrieves list of report dimension values for a list of filters.</summary>
    [HttpPost("userprofiles/{profileId}/dimensionvalues/query")]
    public async Task<IActionResult> QueryDimensionValueAsync([FromRoute] string profileId, [FromBody] QueryDimensionValueRequest request)
    {
        request.ProfileId = profileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _dimensionValueService.QueryDimensionValueAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
