using ApiToolKit.Application.DTOs.Shopee.Livestream;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/livestream")]
[Produces("application/json")]
public class ShopeeLivestreamController : ControllerBase
{
    private readonly IShopeeLivestreamService _livestreamService;

    public ShopeeLivestreamController(IShopeeLivestreamService livestreamService)
    {
        _livestreamService = livestreamService;
    }

    /// <summary>Create a new livestream session</summary>
    [HttpPost]
    public async Task<IActionResult> CreateLivestream([FromBody] CreateLivestreamRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.CreateLivestreamAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get details of a specific livestream session</summary>
    [HttpGet("detail")]
    public async Task<IActionResult> GetLivestreamDetail([FromQuery] GetLivestreamDetailRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.GetLivestreamDetailAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a livestream session's information</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateLivestream([FromBody] UpdateLivestreamRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.UpdateLivestreamAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>End an active livestream session</summary>
    [HttpPost("end")]
    public async Task<IActionResult> EndLivestream([FromBody] EndLivestreamRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.EndLivestreamAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the list of livestream sessions for a shop</summary>
    [HttpGet("list")]
    public async Task<IActionResult> GetLivestreamList([FromQuery] GetLivestreamListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.GetLivestreamListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add products to a livestream session</summary>
    [HttpPost("products")]
    public async Task<IActionResult> AddLivestreamProduct([FromBody] AddLivestreamProductRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.AddLivestreamProductAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove products from a livestream session</summary>
    [HttpDelete("products")]
    public async Task<IActionResult> DeleteLivestreamProduct([FromBody] DeleteLivestreamProductRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.DeleteLivestreamProductAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the list of products in a livestream session</summary>
    [HttpGet("products")]
    public async Task<IActionResult> GetLivestreamProductList([FromQuery] GetLivestreamProductListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.GetLivestreamProductListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Pin a product to the top of the livestream product list</summary>
    [HttpPost("products/pin")]
    public async Task<IActionResult> PinLivestreamProduct([FromBody] PinLivestreamProductRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.PinLivestreamProductAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get performance metrics for a livestream session</summary>
    [HttpGet("metrics")]
    public async Task<IActionResult> GetLivestreamMetrics([FromQuery] GetLivestreamMetricsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.GetLivestreamMetricsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get comments from a livestream session</summary>
    [HttpGet("comments")]
    public async Task<IActionResult> GetLivestreamComment([FromQuery] GetLivestreamCommentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.GetLivestreamCommentAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the replay video list for a livestream session</summary>
    [HttpGet("replays")]
    public async Task<IActionResult> GetLivestreamReplayList([FromQuery] GetLivestreamReplayListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.GetLivestreamReplayListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the cover image details for a livestream session</summary>
    [HttpGet("cover")]
    public async Task<IActionResult> GetLivestreamCover([FromQuery] GetLivestreamCoverRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.GetLivestreamCoverAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Send a notification to followers about an upcoming or active livestream</summary>
    [HttpPost("notification")]
    public async Task<IActionResult> SendLivestreamNotification([FromBody] SendLivestreamNotificationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.SendLivestreamNotificationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the performance report for livestream sessions within a date range</summary>
    [HttpGet("report")]
    public async Task<IActionResult> GetLivestreamReport([FromQuery] GetLivestreamReportRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _livestreamService.GetLivestreamReportAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
