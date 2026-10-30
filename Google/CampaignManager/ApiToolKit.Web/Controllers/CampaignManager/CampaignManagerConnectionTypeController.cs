using ApiToolKit.Application.DTOs.CampaignManager.ConnectionType;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// connectionTypes (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerConnectionTypeController : ControllerBase
{
    private readonly ICampaignManagerConnectionTypeService _connectionTypeService;

    public CampaignManagerConnectionTypeController(ICampaignManagerConnectionTypeService connectionTypeService)
    {
        _connectionTypeService = connectionTypeService;
    }

    /// <summary>Retrieves a list of connection types.</summary>
    [HttpGet("userprofiles/{userprofileId}/connectionTypes")]
    public async Task<IActionResult> ListConnectionTypesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListConnectionTypesRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _connectionTypeService.ListConnectionTypesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one connection type by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/connectionTypes/{connectionTypeId}")]
    public async Task<IActionResult> GetConnectionTypeAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string connectionTypeId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetConnectionTypeRequest { AccountKey = accountKey, UserprofileId = userprofileId, ConnectionTypeId = connectionTypeId };
        var result = await _connectionTypeService.GetConnectionTypeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
