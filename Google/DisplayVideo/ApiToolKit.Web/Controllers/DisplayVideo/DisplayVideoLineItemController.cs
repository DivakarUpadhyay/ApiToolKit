using ApiToolKit.Application.DTOs.DisplayVideo.LineItem;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// lineItems (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoLineItemController : ControllerBase
{
    private readonly IDisplayVideoLineItemService _lineItemService;

    public DisplayVideoLineItemController(IDisplayVideoLineItemService lineItemService)
    {
        _lineItemService = lineItemService;
    }

    /// <summary>Lists assigned targeting options for multiple line items across targeting types.</summary>
    [HttpGet("advertisers/{advertiserId}/lineItems/bulkListAssignedTargetingOptions")]
    public async Task<IActionResult> BulkListAssignedTargetingOptionsLineItemAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null, [FromQuery] string? filter = null, [FromQuery] string? pageToken = null, [FromQuery] List<string>? lineItemIds = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new BulkListAssignedTargetingOptionsLineItemRequest { AccountKey = accountKey, AdvertiserId = advertiserId, PageSize = pageSize, OrderBy = orderBy, Filter = filter, PageToken = pageToken, LineItemIds = lineItemIds };
        var result = await _lineItemService.BulkListAssignedTargetingOptionsLineItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Bulk edits targeting options under multiple line items.</summary>
    [HttpPost("advertisers/{advertiserId}/lineItems/bulkEditAssignedTargetingOptions")]
    public async Task<IActionResult> BulkEditAssignedTargetingOptionsLineItemAsync([FromRoute] string advertiserId, [FromBody] BulkEditAssignedTargetingOptionsLineItemRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _lineItemService.BulkEditAssignedTargetingOptionsLineItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates multiple line items.</summary>
    [HttpPost("advertisers/{advertiserId}/lineItems/bulkUpdate")]
    public async Task<IActionResult> BulkUpdateLineItemAsync([FromRoute] string advertiserId, [FromBody] BulkUpdateLineItemRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _lineItemService.BulkUpdateLineItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a line item.</summary>
    [HttpDelete("advertisers/{advertiserId}/lineItems/{lineItemId}")]
    public async Task<IActionResult> DeleteLineItemAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string lineItemId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteLineItemRequest { AccountKey = accountKey, AdvertiserId = advertiserId, LineItemId = lineItemId };
        var result = await _lineItemService.DeleteLineItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a line item.</summary>
    [HttpGet("advertisers/{advertiserId}/lineItems/{lineItemId}")]
    public async Task<IActionResult> GetLineItemAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string lineItemId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetLineItemRequest { AccountKey = accountKey, AdvertiserId = advertiserId, LineItemId = lineItemId };
        var result = await _lineItemService.GetLineItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new line item.</summary>
    [HttpPost("advertisers/{advertiserId}/lineItems")]
    public async Task<IActionResult> CreateLineItemAsync([FromRoute] string advertiserId, [FromBody] CreateLineItemRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _lineItemService.CreateLineItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing line item.</summary>
    [HttpPatch("advertisers/{advertiserId}/lineItems/{lineItemId}")]
    public async Task<IActionResult> UpdateLineItemAsync([FromRoute] string advertiserId, [FromRoute] string lineItemId, [FromBody] UpdateLineItemRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.LineItemId = lineItemId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _lineItemService.UpdateLineItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Duplicates a line item.</summary>
    [HttpPost("advertisers/{advertiserId}/lineItems/{lineItemId}/duplicate")]
    public async Task<IActionResult> DuplicateLineItemAsync([FromRoute] string advertiserId, [FromRoute] string lineItemId, [FromBody] DuplicateLineItemRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.LineItemId = lineItemId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _lineItemService.DuplicateLineItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists line items in an advertiser.</summary>
    [HttpGet("advertisers/{advertiserId}/lineItems")]
    public async Task<IActionResult> ListLineItemsAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null, [FromQuery] string? filter = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListLineItemsRequest { AccountKey = accountKey, AdvertiserId = advertiserId, PageSize = pageSize, OrderBy = orderBy, Filter = filter, PageToken = pageToken };
        var result = await _lineItemService.ListLineItemsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
