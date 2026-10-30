using ApiToolKit.Application.DTOs.CampaignManager.Size;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// sizes (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerSizeController : ControllerBase
{
    private readonly ICampaignManagerSizeService _sizeService;

    public CampaignManagerSizeController(ICampaignManagerSizeService sizeService)
    {
        _sizeService = sizeService;
    }

    /// <summary>Retrieves a list of sizes, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/sizes")]
    public async Task<IActionResult> ListSizesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] int? height = null, [FromQuery] List<string>? ids = null, [FromQuery] int? width = null, [FromQuery] bool? iabStandard = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListSizesRequest { AccountKey = accountKey, UserprofileId = userprofileId, Height = height, Ids = ids, Width = width, IabStandard = iabStandard };
        var result = await _sizeService.ListSizesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new size.</summary>
    [HttpPost("userprofiles/{userprofileId}/sizes")]
    public async Task<IActionResult> InsertSizeAsync([FromRoute] string userprofileId, [FromBody] InsertSizeRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _sizeService.InsertSizeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one size by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/sizes/{sizeId}")]
    public async Task<IActionResult> GetSizeAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string sizeId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetSizeRequest { AccountKey = accountKey, UserprofileId = userprofileId, SizeId = sizeId };
        var result = await _sizeService.GetSizeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
