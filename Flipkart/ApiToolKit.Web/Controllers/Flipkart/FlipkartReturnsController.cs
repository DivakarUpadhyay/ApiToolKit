using ApiToolKit.Application.DTOs.Flipkart.Returns;
using ApiToolKit.Application.Interfaces.Services.Flipkart;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Flipkart;

[ApiController]
[Route("api/flipkart/returns")]
[Produces("application/json")]
public class FlipkartReturnsController : ControllerBase
{
    private readonly IFlipkartReturnsService _service;
    public FlipkartReturnsController(IFlipkartReturnsService service) => _service = service;

    /// <summary>Get all returns with optional filters</summary>
    [HttpGet]
    public async Task<IActionResult> GetReturns([FromQuery] GetReturnsRequest request)
    {
        var result = await _service.GetReturnsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Approve return requests</summary>
    [HttpPost("approve")]
    public async Task<IActionResult> ApproveReturns([FromBody] ApproveReturnsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.ApproveReturnsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Reject return requests</summary>
    [HttpPost("reject")]
    public async Task<IActionResult> RejectReturns([FromBody] RejectReturnsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.RejectReturnsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Mark returns as complete</summary>
    [HttpPost("complete")]
    public async Task<IActionResult> CompleteReturns([FromBody] CompleteReturnsRequest request)
    {
        var result = await _service.CompleteReturnsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Schedule return pickup</summary>
    [HttpPost("pickup")]
    public async Task<IActionResult> PickupReturns([FromBody] PickupReturnsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.PickupReturnsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Record a pickup attempt</summary>
    [HttpPost("pickup-attempt")]
    public async Task<IActionResult> PickupAttempt([FromBody] PickupAttemptRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.PickupAttemptAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Load initial return tasks</summary>
    [HttpPost("tasks/initial-load")]
    public async Task<IActionResult> InitialReturnLoad([FromBody] InitialReturnLoadRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.InitialReturnLoadAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Begin OTC return process</summary>
    [HttpPost("otc/start")]
    public async Task<IActionResult> StartOtcReturn([FromBody] StartOtcReturnRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.StartOtcReturnAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get scanned shipments for return task</summary>
    [HttpPost("tasks/shipments")]
    public async Task<IActionResult> GetReturnTaskShipments([FromBody] ReturnTaskShipmentsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.GetReturnTaskShipmentsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Complete OTC return process</summary>
    [HttpPost("otc/end")]
    public async Task<IActionResult> EndOtcReturn([FromBody] EndOtcReturnRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.EndOtcReturnAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
