using ApiToolKit.Application.DTOs.Facebook.WhatsApp;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/whatsapp")]
[Produces("application/json")]
public class FacebookWhatsAppController : ControllerBase
{
    private readonly IFacebookWhatsAppService _waService;

    public FacebookWhatsAppController(IFacebookWhatsAppService waService)
    {
        _waService = waService;
    }

    // ─── Messages ────────────────────────────────────────────────────────────

    /// <summary>Send a WhatsApp text message</summary>
    [HttpPost("messages/text")]
    public async Task<IActionResult> SendTextMessageAsync([FromBody] SendTextMessageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendTextMessageAsync(request);
        return Ok(result);
    }

    /// <summary>Send a WhatsApp template message (for outside 24-hour windows)</summary>
    [HttpPost("messages/template")]
    public async Task<IActionResult> SendTemplateMessageAsync([FromBody] SendTemplateMessageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendTemplateMessageAsync(request);
        return Ok(result);
    }

    /// <summary>Send a WhatsApp image message (by media ID or URL)</summary>
    [HttpPost("messages/image")]
    public async Task<IActionResult> SendImageMessageAsync([FromBody] SendImageMessageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendImageMessageAsync(request);
        return Ok(result);
    }

    /// <summary>Send a WhatsApp video message (by media ID or URL)</summary>
    [HttpPost("messages/video")]
    public async Task<IActionResult> SendVideoMessageAsync([FromBody] SendVideoMessageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendVideoMessageAsync(request);
        return Ok(result);
    }

    /// <summary>Send a WhatsApp audio message (by media ID or URL)</summary>
    [HttpPost("messages/audio")]
    public async Task<IActionResult> SendAudioMessageAsync([FromBody] SendAudioMessageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendAudioMessageAsync(request);
        return Ok(result);
    }

    /// <summary>Send a WhatsApp document message (PDF, DOCX, etc.)</summary>
    [HttpPost("messages/document")]
    public async Task<IActionResult> SendDocumentMessageAsync([FromBody] SendDocumentMessageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendDocumentMessageAsync(request);
        return Ok(result);
    }

    /// <summary>Send a WhatsApp sticker message</summary>
    [HttpPost("messages/sticker")]
    public async Task<IActionResult> SendStickerMessageAsync([FromBody] SendStickerMessageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendStickerMessageAsync(request);
        return Ok(result);
    }

    /// <summary>Send a WhatsApp location message</summary>
    [HttpPost("messages/location")]
    public async Task<IActionResult> SendLocationMessageAsync([FromBody] SendLocationMessageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendLocationMessageAsync(request);
        return Ok(result);
    }

    /// <summary>Send a WhatsApp contact card message</summary>
    [HttpPost("messages/contact")]
    public async Task<IActionResult> SendContactMessageAsync([FromBody] SendContactMessageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendContactMessageAsync(request);
        return Ok(result);
    }

    /// <summary>React to a WhatsApp message with an emoji</summary>
    [HttpPost("messages/reaction")]
    public async Task<IActionResult> SendReactionMessageAsync([FromBody] SendReactionMessageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendReactionMessageAsync(request);
        return Ok(result);
    }

    /// <summary>Send interactive reply-button message (up to 3 buttons)</summary>
    [HttpPost("messages/interactive/buttons")]
    public async Task<IActionResult> SendInteractiveButtonsAsync([FromBody] SendInteractiveButtonsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendInteractiveButtonsAsync(request);
        return Ok(result);
    }

    /// <summary>Send interactive list message (sections with rows)</summary>
    [HttpPost("messages/interactive/list")]
    public async Task<IActionResult> SendInteractiveListAsync([FromBody] SendInteractiveListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendInteractiveListAsync(request);
        return Ok(result);
    }

    /// <summary>Send interactive single-product message from a catalog</summary>
    [HttpPost("messages/interactive/product")]
    public async Task<IActionResult> SendInteractiveProductAsync([FromBody] SendInteractiveProductRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendInteractiveProductAsync(request);
        return Ok(result);
    }

    /// <summary>Send interactive multi-product catalog message</summary>
    [HttpPost("messages/interactive/catalog")]
    public async Task<IActionResult> SendInteractiveCatalogAsync([FromBody] SendInteractiveCatalogRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendInteractiveCatalogAsync(request);
        return Ok(result);
    }

    /// <summary>Mark a WhatsApp message as read</summary>
    [HttpPost("messages/read")]
    public async Task<IActionResult> MarkMessageReadAsync([FromBody] MarkMessageReadRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.MarkMessageReadAsync(request);
        return Ok(result);
    }

    // ─── Media ───────────────────────────────────────────────────────────────

    /// <summary>Upload media to WhatsApp servers (returns media ID for use in messages)</summary>
    [HttpPost("media/upload")]
    public async Task<IActionResult> UploadMediaAsync([FromBody] WaUploadMediaRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.UploadMediaAsync(request);
        return Ok(result);
    }

    /// <summary>Retrieve the URL for a previously uploaded media asset</summary>
    [HttpGet("media/{mediaId}")]
    public async Task<IActionResult> GetMediaAsync(string mediaId, [FromQuery] string accessToken)
    {
        var result = await _waService.GetMediaAsync(mediaId, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a previously uploaded media asset from WhatsApp servers</summary>
    [HttpDelete("media/{mediaId}")]
    public async Task<IActionResult> DeleteMediaAsync(string mediaId, [FromQuery] string accessToken)
    {
        var result = await _waService.DeleteMediaAsync(mediaId, accessToken);
        return Ok(result);
    }

    // ─── Phone Numbers ────────────────────────────────────────────────────────

    /// <summary>List all phone numbers associated with a WhatsApp Business Account (WABA)</summary>
    [HttpGet("phone-numbers")]
    public async Task<IActionResult> GetPhoneNumbersAsync([FromQuery] string wabaId, [FromQuery] string accessToken)
    {
        var result = await _waService.GetPhoneNumbersAsync(wabaId, accessToken);
        return Ok(result);
    }

    /// <summary>Get details of a specific phone number</summary>
    [HttpGet("phone-numbers/{phoneNumberId}")]
    public async Task<IActionResult> GetPhoneNumberAsync(string phoneNumberId, [FromQuery] string accessToken)
    {
        var result = await _waService.GetPhoneNumberAsync(phoneNumberId, accessToken);
        return Ok(result);
    }

    /// <summary>Request a verification code via SMS or voice call</summary>
    [HttpPost("phone-numbers/{phoneNumberId}/request-code")]
    public async Task<IActionResult> RequestVerificationCodeAsync(string phoneNumberId, [FromBody] RequestVerificationCodeRequest request)
    {
        request.PhoneNumberId = phoneNumberId;
        var result = await _waService.RequestVerificationCodeAsync(request);
        return Ok(result);
    }

    /// <summary>Verify a phone number with the received code</summary>
    [HttpPost("phone-numbers/{phoneNumberId}/verify-code")]
    public async Task<IActionResult> VerifyCodeAsync(string phoneNumberId, [FromBody] VerifyCodeRequest request)
    {
        request.PhoneNumberId = phoneNumberId;
        var result = await _waService.VerifyCodeAsync(request);
        return Ok(result);
    }

    /// <summary>Register a phone number with the WhatsApp Business Platform</summary>
    [HttpPost("phone-numbers/{phoneNumberId}/register")]
    public async Task<IActionResult> RegisterPhoneNumberAsync(string phoneNumberId, [FromBody] RegisterPhoneRequest request)
    {
        request.PhoneNumberId = phoneNumberId;
        var result = await _waService.RegisterPhoneNumberAsync(request);
        return Ok(result);
    }

    /// <summary>Deregister a phone number from the WhatsApp Business Platform</summary>
    [HttpPost("phone-numbers/{phoneNumberId}/deregister")]
    public async Task<IActionResult> DeregisterPhoneNumberAsync(string phoneNumberId, [FromBody] DeregisterPhoneRequest request)
    {
        request.PhoneNumberId = phoneNumberId;
        var result = await _waService.DeregisterPhoneNumberAsync(request);
        return Ok(result);
    }

    /// <summary>Set or update the two-step verification PIN for a phone number</summary>
    [HttpPost("phone-numbers/{phoneNumberId}/2fa")]
    public async Task<IActionResult> UpdatePinAsync(string phoneNumberId, [FromBody] UpdatePinRequest request)
    {
        request.PhoneNumberId = phoneNumberId;
        var result = await _waService.UpdatePinAsync(request);
        return Ok(result);
    }

    // ─── Business Profile ─────────────────────────────────────────────────────

    /// <summary>Get the WhatsApp Business Profile for a phone number</summary>
    [HttpGet("business-profile")]
    public async Task<IActionResult> GetBusinessProfileAsync([FromQuery] string phoneNumberId, [FromQuery] string accessToken)
    {
        var result = await _waService.GetBusinessProfileAsync(phoneNumberId, accessToken);
        return Ok(result);
    }

    /// <summary>Update the WhatsApp Business Profile (about, address, email, websites, etc.)</summary>
    [HttpPost("business-profile")]
    public async Task<IActionResult> UpdateBusinessProfileAsync([FromBody] UpdateBusinessProfileRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.UpdateBusinessProfileAsync(request);
        return Ok(result);
    }

    // ─── Templates ────────────────────────────────────────────────────────────

    /// <summary>Create a new message template (requires Facebook approval)</summary>
    [HttpPost("templates")]
    public async Task<IActionResult> CreateTemplateAsync([FromBody] CreateTemplateRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.CreateTemplateAsync(request);
        return Ok(result);
    }

    /// <summary>List all message templates for a WABA</summary>
    [HttpGet("templates")]
    public async Task<IActionResult> GetTemplatesAsync([FromQuery] string wabaId, [FromQuery] string accessToken)
    {
        var result = await _waService.GetTemplatesAsync(wabaId, accessToken);
        return Ok(result);
    }

    /// <summary>Get a specific message template by ID</summary>
    [HttpGet("templates/{templateId}")]
    public async Task<IActionResult> GetTemplateAsync(string templateId, [FromQuery] string accessToken)
    {
        var result = await _waService.GetTemplateAsync(templateId, accessToken);
        return Ok(result);
    }

    /// <summary>Update an existing message template</summary>
    [HttpPut("templates/{templateId}")]
    public async Task<IActionResult> UpdateTemplateAsync(string templateId, [FromBody] UpdateTemplateRequest request)
    {
        request.TemplateId = templateId;
        var result = await _waService.UpdateTemplateAsync(request);
        return Ok(result);
    }

    /// <summary>Delete one language version of a message template (needs the WABA ID and the template name)</summary>
    [HttpDelete("templates/{templateId}")]
    public async Task<IActionResult> DeleteTemplateAsync(string templateId, [FromQuery] string wabaId, [FromQuery] string name, [FromQuery] string accessToken)
    {
        var result = await _waService.DeleteTemplateAsync(wabaId, templateId, name, accessToken);
        return Ok(result);
    }

    // ─── QR Codes ─────────────────────────────────────────────────────────────

    /// <summary>Create a QR code with a prefilled message for a phone number</summary>
    [HttpPost("qr-codes")]
    public async Task<IActionResult> CreateQrCodeAsync([FromBody] CreateQrCodeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.CreateQrCodeAsync(request);
        return Ok(result);
    }

    /// <summary>List all QR codes for a phone number</summary>
    [HttpGet("qr-codes")]
    public async Task<IActionResult> GetQrCodesAsync([FromQuery] string phoneNumberId, [FromQuery] string accessToken)
    {
        var result = await _waService.GetQrCodesAsync(phoneNumberId, accessToken);
        return Ok(result);
    }

    /// <summary>Get a specific QR code</summary>
    [HttpGet("qr-codes/{qrCodeId}")]
    public async Task<IActionResult> GetQrCodeAsync(string qrCodeId, [FromQuery] string phoneNumberId, [FromQuery] string accessToken)
    {
        var result = await _waService.GetQrCodeAsync(phoneNumberId, qrCodeId, accessToken);
        return Ok(result);
    }

    /// <summary>Update the prefilled message of a QR code</summary>
    [HttpPut("qr-codes/{qrCodeId}")]
    public async Task<IActionResult> UpdateQrCodeAsync(string qrCodeId, [FromBody] UpdateQrCodeRequest request)
    {
        request.QrCodeId = qrCodeId;
        var result = await _waService.UpdateQrCodeAsync(request);
        return Ok(result);
    }

    /// <summary>Delete a QR code</summary>
    [HttpDelete("qr-codes/{qrCodeId}")]
    public async Task<IActionResult> DeleteQrCodeAsync(string qrCodeId, [FromQuery] string phoneNumberId, [FromQuery] string accessToken)
    {
        var result = await _waService.DeleteQrCodeAsync(phoneNumberId, qrCodeId, accessToken);
        return Ok(result);
    }

    // ─── Webhooks ─────────────────────────────────────────────────────────────

    /// <summary>Subscribe app to WhatsApp webhook notifications</summary>
    [HttpPost("webhook/subscribe")]
    public async Task<IActionResult> SubscribeWebhookAsync([FromBody] WaWebhookSubscribeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SubscribeWebhookAsync(request);
        return Ok(result);
    }

    /// <summary>WhatsApp webhook verification challenge (GET from Facebook)</summary>
    [HttpGet("webhook/verify")]
    public async Task<IActionResult> VerifyWebhookAsync(
        [FromQuery(Name = "hub.mode")] string hubMode,
        [FromQuery(Name = "hub.challenge")] string hubChallenge,
        [FromQuery(Name = "hub.verify_token")] string hubVerifyToken)
    {
        var result = await _waService.VerifyWebhookAsync(hubMode, hubChallenge, hubVerifyToken);
        if (result.Success) return Content(result.Data ?? string.Empty);
        return Ok(result);
    }

    /// <summary>Receive incoming WhatsApp webhook events (messages, status updates)</summary>
    [HttpPost("webhook/payload")]
    public async Task<IActionResult> ProcessWebhookPayloadAsync([FromBody] WaWebhookPayload payload)
    {
        var result = await _waService.ProcessWebhookPayloadAsync(payload);
        return Ok(result);
    }

    /// <summary>Send an interactive message requesting the recipient's location</summary>
    [HttpPost("messages/interactive/location-request")]
    public async Task<IActionResult> SendInteractiveLocationRequestAsync([FromBody] SendInteractiveLocationRequestRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendInteractiveLocationRequestAsync(request);
        return Ok(result);
    }

    /// <summary>Send an interactive CTA URL button message</summary>
    [HttpPost("messages/interactive/cta-url")]
    public async Task<IActionResult> SendInteractiveCtaUrlAsync([FromBody] SendInteractiveCtaUrlRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendInteractiveCtaUrlAsync(request);
        return Ok(result);
    }

    /// <summary>Send a WhatsApp Flow interactive message</summary>
    [HttpPost("messages/interactive/flow")]
    public async Task<IActionResult> SendInteractiveFlowAsync([FromBody] SendInteractiveFlowRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.SendInteractiveFlowAsync(request);
        return Ok(result);
    }

    /// <summary>Get WhatsApp Business Account (WABA) info</summary>
    [HttpGet("waba/{wabaId}")]
    public async Task<IActionResult> GetWabaInfoAsync(string wabaId, [FromQuery] string accessToken)
    {
        var result = await _waService.GetWabaInfoAsync(wabaId, accessToken);
        return Ok(result);
    }

    /// <summary>Get WhatsApp message analytics (sent/delivered/read counts)</summary>
    [HttpPost("analytics")]
    public async Task<IActionResult> GetAnalyticsAsync([FromBody] WaAnalyticsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.GetAnalyticsAsync(request);
        return Ok(result);
    }

    /// <summary>Get WhatsApp conversation analytics (cost and conversation breakdown)</summary>
    [HttpPost("conversation-analytics")]
    public async Task<IActionResult> GetConversationAnalyticsAsync([FromBody] WaConversationAnalyticsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _waService.GetConversationAnalyticsAsync(request);
        return Ok(result);
    }
}
