using ApiToolKit.Application.DTOs.Facebook.Capi;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;
namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/capi")]
[Produces("application/json")]
public class FacebookCapiController : ControllerBase {
    private readonly IFacebookCapiService _service;
    public FacebookCapiController(IFacebookCapiService service) => _service = service;

    /// <summary>Send custom Conversions API events to Meta for the specified pixel</summary>
    [HttpPost("events")]
    public async Task<IActionResult> SendEvents([FromBody] SendCApiEventsRequest request) {
        var result = await _service.SendEventsAsync(request);
        return Ok(result);
    }

    /// <summary>Send a Purchase conversion event via the Meta Conversions API</summary>
    [HttpPost("events/purchase")]
    public async Task<IActionResult> SendPurchaseEvent([FromBody] PurchaseEventRequest request) {
        var result = await _service.SendPurchaseEventAsync(request);
        return Ok(result);
    }

    /// <summary>Send an AddToCart conversion event via the Meta Conversions API</summary>
    [HttpPost("events/add-to-cart")]
    public async Task<IActionResult> SendAddToCartEvent([FromBody] AddToCartEventRequest request) {
        var result = await _service.SendAddToCartEventAsync(request);
        return Ok(result);
    }

    /// <summary>Send a PageView conversion event via the Meta Conversions API</summary>
    [HttpPost("events/page-view")]
    public async Task<IActionResult> SendPageViewEvent([FromBody] PageViewEventRequest request) {
        var result = await _service.SendPageViewEventAsync(request);
        return Ok(result);
    }

    /// <summary>Send a Lead conversion event via the Meta Conversions API</summary>
    [HttpPost("events/lead")]
    public async Task<IActionResult> SendLeadEvent([FromBody] LeadEventRequest request) {
        var result = await _service.SendLeadEventAsync(request);
        return Ok(result);
    }
}
