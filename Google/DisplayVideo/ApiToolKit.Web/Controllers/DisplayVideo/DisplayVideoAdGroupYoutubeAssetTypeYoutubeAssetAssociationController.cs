using ApiToolKit.Application.DTOs.DisplayVideo.AdGroupYoutubeAssetTypeYoutubeAssetAssociation;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// adGroups / youtubeAssetTypes / youtubeAssetAssociations (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoAdGroupYoutubeAssetTypeYoutubeAssetAssociationController : ControllerBase
{
    private readonly IDisplayVideoAdGroupYoutubeAssetTypeYoutubeAssetAssociationService _adGroupYoutubeAssetTypeYoutubeAssetAssociationService;

    public DisplayVideoAdGroupYoutubeAssetTypeYoutubeAssetAssociationController(IDisplayVideoAdGroupYoutubeAssetTypeYoutubeAssetAssociationService adGroupYoutubeAssetTypeYoutubeAssetAssociationService)
    {
        _adGroupYoutubeAssetTypeYoutubeAssetAssociationService = adGroupYoutubeAssetTypeYoutubeAssetAssociationService;
    }

    /// <summary>Lists the YouTube asset associations linked to the given resource.</summary>
    [HttpGet("advertisers/{advertiserId}/adGroups/{adGroupId}/youtubeAssetTypes/{youtubeAssetTypeId}/youtubeAssetAssociations")]
    public async Task<IActionResult> ListYoutubeAssetAssociationsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string adGroupId, [FromRoute] string youtubeAssetTypeId, [FromQuery] string? linkedEntityLineItemId = null, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListYoutubeAssetAssociationsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, AdGroupId = adGroupId, YoutubeAssetTypeId = youtubeAssetTypeId, LinkedEntityLineItemId = linkedEntityLineItemId, PageSize = pageSize, OrderBy = orderBy, PageToken = pageToken };
        var result = await _adGroupYoutubeAssetTypeYoutubeAssetAssociationService.ListYoutubeAssetAssociationsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new association between the identified resource and a YouTube asset.</summary>
    [HttpPost("advertisers/{advertiserId}/adGroups/{adGroupId}/youtubeAssetTypes/{youtubeAssetTypeId}/youtubeAssetAssociations")]
    public async Task<IActionResult> CreateYoutubeAssetAssociationAsync([FromRoute] string advertiserId, [FromRoute] string adGroupId, [FromRoute] string youtubeAssetTypeId, [FromBody] CreateYoutubeAssetAssociationRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.AdGroupId = adGroupId;
        request.YoutubeAssetTypeId = youtubeAssetTypeId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adGroupYoutubeAssetTypeYoutubeAssetAssociationService.CreateYoutubeAssetAssociationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an existing association between the identified resource and a YouTube asset.</summary>
    [HttpDelete("advertisers/{advertiserId}/adGroups/{adGroupId}/youtubeAssetTypes/{youtubeAssetTypeId}/youtubeAssetAssociations/{youtubeAssetAssociationId}")]
    public async Task<IActionResult> DeleteYoutubeAssetAssociationAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string adGroupId, [FromRoute] string youtubeAssetTypeId, [FromRoute] string youtubeAssetAssociationId, [FromQuery] string? linkedEntityLineItemId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteYoutubeAssetAssociationRequest { AccountKey = accountKey, AdvertiserId = advertiserId, AdGroupId = adGroupId, YoutubeAssetTypeId = youtubeAssetTypeId, YoutubeAssetAssociationId = youtubeAssetAssociationId, LinkedEntityLineItemId = linkedEntityLineItemId };
        var result = await _adGroupYoutubeAssetTypeYoutubeAssetAssociationService.DeleteYoutubeAssetAssociationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
