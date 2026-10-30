using ApiToolKit.Application.DTOs.DisplayVideo.GuaranteedOrder;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// guaranteedOrders (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoGuaranteedOrderController : ControllerBase
{
    private readonly IDisplayVideoGuaranteedOrderService _guaranteedOrderService;

    public DisplayVideoGuaranteedOrderController(IDisplayVideoGuaranteedOrderService guaranteedOrderService)
    {
        _guaranteedOrderService = guaranteedOrderService;
    }

    /// <summary>Gets a guaranteed order.</summary>
    [HttpGet("guaranteedOrders/{guaranteedOrderId}")]
    public async Task<IActionResult> GetGuaranteedOrderAsync([FromQuery] string accountKey, [FromRoute] string guaranteedOrderId, [FromQuery] string? partnerId = null, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetGuaranteedOrderRequest { AccountKey = accountKey, GuaranteedOrderId = guaranteedOrderId, PartnerId = partnerId, AdvertiserId = advertiserId };
        var result = await _guaranteedOrderService.GetGuaranteedOrderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Edits read advertisers of a guaranteed order.</summary>
    [HttpPost("guaranteedOrders/{guaranteedOrderId}/editGuaranteedOrderReadAccessors")]
    public async Task<IActionResult> EditGuaranteedOrderReadAccessorsAsync([FromRoute] string guaranteedOrderId, [FromBody] EditGuaranteedOrderReadAccessorsRequest request)
    {
        request.GuaranteedOrderId = guaranteedOrderId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _guaranteedOrderService.EditGuaranteedOrderReadAccessorsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing guaranteed order.</summary>
    [HttpPatch("guaranteedOrders/{guaranteedOrderId}")]
    public async Task<IActionResult> UpdateGuaranteedOrderAsync([FromRoute] string guaranteedOrderId, [FromBody] UpdateGuaranteedOrderRequest request)
    {
        request.GuaranteedOrderId = guaranteedOrderId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _guaranteedOrderService.UpdateGuaranteedOrderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists guaranteed orders that are accessible to the current user.</summary>
    [HttpGet("guaranteedOrders")]
    public async Task<IActionResult> ListGuaranteedOrdersAsync([FromQuery] string accountKey, [FromQuery] string? advertiserId = null, [FromQuery] string? filter = null, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListGuaranteedOrdersRequest { AccountKey = accountKey, AdvertiserId = advertiserId, Filter = filter, PartnerId = partnerId, PageToken = pageToken, PageSize = pageSize, OrderBy = orderBy };
        var result = await _guaranteedOrderService.ListGuaranteedOrdersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new guaranteed order.</summary>
    [HttpPost("guaranteedOrders")]
    public async Task<IActionResult> CreateGuaranteedOrderAsync([FromBody] CreateGuaranteedOrderRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _guaranteedOrderService.CreateGuaranteedOrderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
