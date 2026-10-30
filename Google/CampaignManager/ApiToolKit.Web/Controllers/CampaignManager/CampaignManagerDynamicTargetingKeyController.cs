using ApiToolKit.Application.DTOs.CampaignManager.DynamicTargetingKey;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// dynamicTargetingKeys (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerDynamicTargetingKeyController : ControllerBase
{
    private readonly ICampaignManagerDynamicTargetingKeyService _dynamicTargetingKeyService;

    public CampaignManagerDynamicTargetingKeyController(ICampaignManagerDynamicTargetingKeyService dynamicTargetingKeyService)
    {
        _dynamicTargetingKeyService = dynamicTargetingKeyService;
    }

    /// <summary>Deletes an existing dynamic targeting key.</summary>
    [HttpDelete("userprofiles/{userprofileId}/dynamicTargetingKeys/{dynamicTargetingKeyId}")]
    public async Task<IActionResult> DeleteDynamicTargetingKeyAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string dynamicTargetingKeyId, [FromQuery] string name, [FromQuery] string objectType)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(name)) return BadRequest("name is required.");
        if (string.IsNullOrEmpty(objectType)) return BadRequest("objectType is required.");
        var request = new DeleteDynamicTargetingKeyRequest { AccountKey = accountKey, UserprofileId = userprofileId, DynamicTargetingKeyId = dynamicTargetingKeyId, Name = name, ObjectType = objectType };
        var result = await _dynamicTargetingKeyService.DeleteDynamicTargetingKeyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Inserts a new dynamic targeting key.</summary>
    [HttpPost("userprofiles/{userprofileId}/dynamicTargetingKeys")]
    public async Task<IActionResult> InsertDynamicTargetingKeyAsync([FromRoute] string userprofileId, [FromBody] InsertDynamicTargetingKeyRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _dynamicTargetingKeyService.InsertDynamicTargetingKeyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of dynamic targeting keys.</summary>
    [HttpGet("userprofiles/{userprofileId}/dynamicTargetingKeys")]
    public async Task<IActionResult> ListDynamicTargetingKeysAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? objectId = null, [FromQuery] List<string>? names = null, [FromQuery] string? objectType = null, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListDynamicTargetingKeysRequest { AccountKey = accountKey, UserprofileId = userprofileId, ObjectId = objectId, Names = names, ObjectType = objectType, AdvertiserId = advertiserId };
        var result = await _dynamicTargetingKeyService.ListDynamicTargetingKeysAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
