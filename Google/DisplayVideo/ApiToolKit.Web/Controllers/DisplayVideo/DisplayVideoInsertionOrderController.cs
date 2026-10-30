using ApiToolKit.Application.DTOs.DisplayVideo.InsertionOrder;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// insertionOrders (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoInsertionOrderController : ControllerBase
{
    private readonly IDisplayVideoInsertionOrderService _insertionOrderService;

    public DisplayVideoInsertionOrderController(IDisplayVideoInsertionOrderService insertionOrderService)
    {
        _insertionOrderService = insertionOrderService;
    }

    /// <summary>Creates a new insertion order.</summary>
    [HttpPost("advertisers/{advertiserId}/insertionOrders")]
    public async Task<IActionResult> CreateInsertionOrderAsync([FromRoute] string advertiserId, [FromBody] CreateInsertionOrderRequest request)
    {
        request.AdvertiserId = advertiserId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _insertionOrderService.CreateInsertionOrderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists insertion orders in an advertiser.</summary>
    [HttpGet("advertisers/{advertiserId}/insertionOrders")]
    public async Task<IActionResult> ListInsertionOrdersAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] string? filter = null, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListInsertionOrdersRequest { AccountKey = accountKey, AdvertiserId = advertiserId, Filter = filter, PageToken = pageToken, PageSize = pageSize, OrderBy = orderBy };
        var result = await _insertionOrderService.ListInsertionOrdersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing insertion order.</summary>
    [HttpPatch("advertisers/{advertiserId}/insertionOrders/{insertionOrderId}")]
    public async Task<IActionResult> UpdateInsertionOrderAsync([FromRoute] string advertiserId, [FromRoute] string insertionOrderId, [FromBody] UpdateInsertionOrderRequest request)
    {
        request.AdvertiserId = advertiserId;
        request.InsertionOrderId = insertionOrderId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _insertionOrderService.UpdateInsertionOrderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets an insertion order.</summary>
    [HttpGet("advertisers/{advertiserId}/insertionOrders/{insertionOrderId}")]
    public async Task<IActionResult> GetInsertionOrderAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string insertionOrderId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetInsertionOrderRequest { AccountKey = accountKey, AdvertiserId = advertiserId, InsertionOrderId = insertionOrderId };
        var result = await _insertionOrderService.GetInsertionOrderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes an insertion order.</summary>
    [HttpDelete("advertisers/{advertiserId}/insertionOrders/{insertionOrderId}")]
    public async Task<IActionResult> DeleteInsertionOrderAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromRoute] string insertionOrderId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteInsertionOrderRequest { AccountKey = accountKey, AdvertiserId = advertiserId, InsertionOrderId = insertionOrderId };
        var result = await _insertionOrderService.DeleteInsertionOrderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
