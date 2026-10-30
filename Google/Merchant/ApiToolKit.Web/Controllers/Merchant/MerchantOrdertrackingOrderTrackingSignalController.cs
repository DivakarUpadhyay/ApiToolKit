using ApiToolKit.Application.DTOs.Merchant.OrdertrackingOrderTrackingSignal;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// orderTrackingSignals (v1)
/// </summary>
[ApiController]
[Route("api/merchant/ordertracking")]
[Produces("application/json")]
public class MerchantOrdertrackingOrderTrackingSignalController : ControllerBase
{
    private readonly IMerchantOrdertrackingOrderTrackingSignalService _ordertrackingOrderTrackingSignalService;

    public MerchantOrdertrackingOrderTrackingSignalController(IMerchantOrdertrackingOrderTrackingSignalService ordertrackingOrderTrackingSignalService)
    {
        _ordertrackingOrderTrackingSignalService = ordertrackingOrderTrackingSignalService;
    }

    /// <summary>Creates new order tracking signal.</summary>
    [HttpPost("accounts/{accountId}/orderTrackingSignals")]
    public async Task<IActionResult> CreateOrderTrackingSignalAsync([FromRoute] string accountId, [FromBody] CreateOrderTrackingSignalRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _ordertrackingOrderTrackingSignalService.CreateOrderTrackingSignalAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
