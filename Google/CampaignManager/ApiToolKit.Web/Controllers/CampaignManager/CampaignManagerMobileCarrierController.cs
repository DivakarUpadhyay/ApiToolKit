using ApiToolKit.Application.DTOs.CampaignManager.MobileCarrier;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// mobileCarriers (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerMobileCarrierController : ControllerBase
{
    private readonly ICampaignManagerMobileCarrierService _mobileCarrierService;

    public CampaignManagerMobileCarrierController(ICampaignManagerMobileCarrierService mobileCarrierService)
    {
        _mobileCarrierService = mobileCarrierService;
    }

    /// <summary>Gets one mobile carrier by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/mobileCarriers/{mobileCarrierId}")]
    public async Task<IActionResult> GetMobileCarrierAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string mobileCarrierId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetMobileCarrierRequest { AccountKey = accountKey, UserprofileId = userprofileId, MobileCarrierId = mobileCarrierId };
        var result = await _mobileCarrierService.GetMobileCarrierAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of mobile carriers.</summary>
    [HttpGet("userprofiles/{userprofileId}/mobileCarriers")]
    public async Task<IActionResult> ListMobileCarriersAsync([FromQuery] string accountKey, [FromRoute] string userprofileId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListMobileCarriersRequest { AccountKey = accountKey, UserprofileId = userprofileId };
        var result = await _mobileCarrierService.ListMobileCarriersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
