using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/whatsapp/analytics")]
[Produces("application/json")]
public class FacebookWhatsAppAnalyticsController : ControllerBase
{
    private readonly IFacebookWhatsAppAnalyticsService _service;

    public FacebookWhatsAppAnalyticsController(IFacebookWhatsAppAnalyticsService service) => _service = service;

    /// <summary>Pricing analytics (cost and volume by category, type, country, phone) — replaces conversation analytics</summary>
    [HttpPost("pricing")]
    public async Task<IActionResult> GetPricingAnalytics([FromBody] WaPricingAnalyticsRequest request) =>
        Ok(await _service.GetPricingAnalyticsAsync(request));

    /// <summary>Call analytics (count, cost, average duration)</summary>
    [HttpPost("calls")]
    public async Task<IActionResult> GetCallAnalytics([FromBody] WaCallAnalyticsRequest request) =>
        Ok(await _service.GetCallAnalyticsAsync(request));

    /// <summary>Template analytics (sent, delivered, read, clicked, cost) for up to 10 templates</summary>
    [HttpPost("templates")]
    public async Task<IActionResult> GetTemplateAnalytics([FromBody] WaTemplateAnalyticsRequest request) =>
        Ok(await _service.GetTemplateAnalyticsAsync(request));

    /// <summary>Template group analytics for up to 10 template groups</summary>
    [HttpPost("template-groups")]
    public async Task<IActionResult> GetTemplateGroupAnalytics([FromBody] WaTemplateGroupAnalyticsRequest request) =>
        Ok(await _service.GetTemplateGroupAnalyticsAsync(request));

    /// <summary>Group analytics (sent, delivered, read, participants joined/left)</summary>
    [HttpPost("groups")]
    public async Task<IActionResult> GetGroupAnalytics([FromBody] WaGroupAnalyticsRequest request) =>
        Ok(await _service.GetGroupAnalyticsAsync(request));

    /// <summary>WhatsApp Flow endpoint metrics (request count, errors, latency, availability)</summary>
    [HttpPost("flows")]
    public async Task<IActionResult> GetFlowMetrics([FromBody] WaFlowMetricsRequest request) =>
        Ok(await _service.GetFlowMetricsAsync(request));
}
