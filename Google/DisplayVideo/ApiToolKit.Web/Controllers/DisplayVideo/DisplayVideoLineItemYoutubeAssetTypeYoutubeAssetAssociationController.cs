using ApiToolKit.Application.DTOs.DisplayVideo.LineItemYoutubeAssetTypeYoutubeAssetAssociation;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// lineItems / youtubeAssetTypes / youtubeAssetAssociations (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoLineItemYoutubeAssetTypeYoutubeAssetAssociationController : ControllerBase
{
    private readonly IDisplayVideoLineItemYoutubeAssetTypeYoutubeAssetAssociationService _lineItemYoutubeAssetTypeYoutubeAssetAssociationService;

    public DisplayVideoLineItemYoutubeAssetTypeYoutubeAssetAssociationController(IDisplayVideoLineItemYoutubeAssetTypeYoutubeAssetAssociationService lineItemYoutubeAssetTypeYoutubeAssetAssociationService)
    {
        _lineItemYoutubeAssetTypeYoutubeAssetAssociationService = lineItemYoutubeAssetTypeYoutubeAssetAssociationService;
    }

    /// <summary>Lists the YouTube asset associations linked to the given resource.</summary>
    [HttpGet("advertisers/{advertiserId}/lineItems/{lineItemId}/youtubeAssetTypes/{youtubeAssetTypeId}/youtubeAssetAssociations")]
    public async Task<IActionResult> ListYoutubeAssetAssociationsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string lineItemId, [FromRoute] string youtubeAssetTypeId, [FromQuery] string? linkedEntityAdGroupId = null, [FromQuery] string? orderBy = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListYoutubeAssetAssociationsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, LineItemId = lineItemId, YoutubeAssetTypeId = youtubeAssetTypeId, LinkedEntityAdGroupId = linkedEntityAdGroupId, OrderBy = orderBy, PageSize = pageSize, PageToken = pageToken };
        var result = await _lineItemYoutubeAssetTypeYoutubeAssetAssociationService.ListYoutubeAssetAssociationsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new association between the identified resource and a YouTube asset.</summary>
    [HttpPost("advertisers/{advertiserId}/lineItems/{lineItemId}/youtubeAssetTypes/{youtubeAssetTypeId}/youtubeAssetAssociations")]
    public async Task<IActionResult> CreateYoutubeAssetAssociationAsync([FromRoute] string advertiserId, [FromRoute] string lineItemId, [FromRoute] string youtubeAssetTypeId, [FromBody] CreateYoutubeAssetAssociationRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.LineItemId = lineItemId;
        request.YoutubeAssetTypeId = youtubeAssetTypeId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _lineItemYoutubeAssetTypeYoutubeAssetAssociationService.CreateYoutubeAssetAssociationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an existing association between the identified resource and a YouTube asset.</summary>
    [HttpDelete("advertisers/{advertiserId}/lineItems/{lineItemId}/youtubeAssetTypes/{youtubeAssetTypeId}/youtubeAssetAssociations/{youtubeAssetAssociationId}")]
    public async Task<IActionResult> DeleteYoutubeAssetAssociationAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string lineItemId, [FromRoute] string youtubeAssetTypeId, [FromRoute] string youtubeAssetAssociationId, [FromQuery] string? linkedEntityAdGroupId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteYoutubeAssetAssociationRequest { AccountKey = accountKey, AdvertiserId = advertiserId, LineItemId = lineItemId, YoutubeAssetTypeId = youtubeAssetTypeId, YoutubeAssetAssociationId = youtubeAssetAssociationId, LinkedEntityAdGroupId = linkedEntityAdGroupId };
        var result = await _lineItemYoutubeAssetTypeYoutubeAssetAssociationService.DeleteYoutubeAssetAssociationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
