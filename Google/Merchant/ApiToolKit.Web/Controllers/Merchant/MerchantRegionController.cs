using ApiToolKit.Application.DTOs.Merchant.Region;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// regions (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantRegionController : ControllerBase
{
    private readonly IMerchantRegionService _regionService;

    public MerchantRegionController(IMerchantRegionService regionService)
    {
        _regionService = regionService;
    }

    /// <summary>Deletes a region definition from your Merchant Center account.</summary>
    [HttpDelete("accounts/{accountId}/regions/{regionId}")]
    public async Task<IActionResult> DeleteRegionAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string regionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteRegionRequest { AccountKey = accountKey, AccountId = accountId, RegionId = regionId };
        var result = await _regionService.DeleteRegionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates one or more regions in your Merchant Center account.</summary>
    [HttpPost("accounts/{accountId}/regions/batchCreate")]
    public async Task<IActionResult> BatchCreateRegionsAsync([FromRoute] string accountId, [FromBody] BatchCreateRegionsRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _regionService.BatchCreateRegionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates a region definition in your Merchant Center account.</summary>
    [HttpPatch("accounts/{accountId}/regions/{regionId}")]
    public async Task<IActionResult> UpdateRegionAsync([FromRoute] string accountId, [FromRoute] string regionId, [FromBody] UpdateRegionRequest request)
    {
        request.AccountId = accountId;
        request.RegionId = regionId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _regionService.UpdateRegionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists the regions in your Merchant Center account.</summary>
    [HttpGet("accounts/{accountId}/regions")]
    public async Task<IActionResult> ListRegionsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListRegionsRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _regionService.ListRegionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates one or more regions in your Merchant Center account.</summary>
    [HttpPost("accounts/{accountId}/regions/batchUpdate")]
    public async Task<IActionResult> BatchUpdateRegionsAsync([FromRoute] string accountId, [FromBody] BatchUpdateRegionsRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _regionService.BatchUpdateRegionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes multiple regions by name from your Merchant Center account.</summary>
    [HttpPost("accounts/{accountId}/regions/batchDelete")]
    public async Task<IActionResult> BatchDeleteRegionsAsync([FromRoute] string accountId, [FromBody] BatchDeleteRegionsRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _regionService.BatchDeleteRegionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a region defined in your Merchant Center account.</summary>
    [HttpGet("accounts/{accountId}/regions/{regionId}")]
    public async Task<IActionResult> GetRegionAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string regionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetRegionRequest { AccountKey = accountKey, AccountId = accountId, RegionId = regionId };
        var result = await _regionService.GetRegionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a region definition in your Merchant Center account.</summary>
    [HttpPost("accounts/{accountId}/regions")]
    public async Task<IActionResult> CreateRegionAsync([FromRoute] string accountId, [FromBody] CreateRegionRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _regionService.CreateRegionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
