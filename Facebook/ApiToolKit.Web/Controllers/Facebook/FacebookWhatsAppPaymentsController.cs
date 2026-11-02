using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/whatsapp/payments")]
[Produces("application/json")]
public class FacebookWhatsAppPaymentsController : ControllerBase
{
    private readonly IFacebookWhatsAppPaymentsService _service;

    public FacebookWhatsAppPaymentsController(IFacebookWhatsAppPaymentsService service) => _service = service;

    // ─── Payment configurations (India) ─────────────────────────────────────

    /// <summary>List payment configurations linked to a WABA</summary>
    [HttpGet("configurations")]
    public async Task<IActionResult> GetConfigurations([FromQuery] string wabaId, [FromQuery] string accessToken) =>
        Ok(await _service.GetPaymentConfigurationsAsync(wabaId, accessToken));

    /// <summary>Get one payment configuration</summary>
    [HttpGet("configurations/{configurationName}")]
    public async Task<IActionResult> GetConfiguration(string configurationName, [FromQuery] string wabaId, [FromQuery] string accessToken) =>
        Ok(await _service.GetPaymentConfigurationAsync(wabaId, configurationName, accessToken));

    /// <summary>Create a payment configuration (UPI VPA or payment gateway)</summary>
    [HttpPost("configurations")]
    public async Task<IActionResult> CreateConfiguration([FromBody] WaCreatePaymentConfigRequest request) =>
        Ok(await _service.CreatePaymentConfigurationAsync(request));

    /// <summary>Link or update the data endpoint of a payment configuration</summary>
    [HttpPost("configurations/{configurationName}/data-endpoint")]
    public async Task<IActionResult> UpdateDataEndpoint(string configurationName, [FromBody] WaPaymentConfigDataEndpointRequest request)
    {
        request.ConfigurationName = configurationName;
        return Ok(await _service.UpdatePaymentDataEndpointAsync(request));
    }

    /// <summary>Regenerate the payment gateway OAuth link</summary>
    [HttpPost("configurations/{configurationName}/oauth-link")]
    public async Task<IActionResult> RegenerateOauthLink(string configurationName, [FromBody] WaRegeneratePaymentOauthLinkRequest request)
    {
        request.ConfigurationName = configurationName;
        return Ok(await _service.RegenerateOauthLinkAsync(request));
    }

    /// <summary>Remove a payment configuration</summary>
    [HttpDelete("configurations/{configurationName}")]
    public async Task<IActionResult> DeleteConfiguration(string configurationName, [FromQuery] string wabaId, [FromQuery] string accessToken) =>
        Ok(await _service.DeletePaymentConfigurationAsync(wabaId, configurationName, accessToken));

    // ─── Payments ───────────────────────────────────────────────────────────

    /// <summary>Look up a payment's status by reference ID</summary>
    [HttpGet("{paymentConfiguration}/{referenceId}")]
    public async Task<IActionResult> GetPaymentStatus(string paymentConfiguration, string referenceId,
        [FromQuery] string phoneNumberId, [FromQuery] string accessToken) =>
        Ok(await _service.GetPaymentStatusAsync(phoneNumberId, paymentConfiguration, referenceId, accessToken));

    /// <summary>Refund a payment</summary>
    [HttpPost("refund")]
    public async Task<IActionResult> Refund([FromBody] WaRefundRequest request) =>
        Ok(await _service.RefundAsync(request));

    /// <summary>Send an order_details (review and pay) message</summary>
    [HttpPost("messages/order-details")]
    public async Task<IActionResult> SendOrderDetails([FromBody] WaOrderDetailsMessageRequest request) =>
        Ok(await _service.SendOrderDetailsAsync(request));

    /// <summary>Send an order_status update message</summary>
    [HttpPost("messages/order-status")]
    public async Task<IActionResult> SendOrderStatus([FromBody] WaOrderStatusMessageRequest request) =>
        Ok(await _service.SendOrderStatusAsync(request));
}
