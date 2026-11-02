using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/whatsapp")]
[Produces("application/json")]
public class FacebookWhatsAppAccountController : ControllerBase
{
    private readonly IFacebookWhatsAppAccountService _service;

    public FacebookWhatsAppAccountController(IFacebookWhatsAppAccountService service) => _service = service;

    // ─── WABA webhooks ───────────────────────────────────────────────────────

    /// <summary>Subscribe your app to a WABA's webhooks (optionally with a per-WABA callback override)</summary>
    [HttpPost("waba/{wabaId}/subscribed-apps")]
    public async Task<IActionResult> SubscribeApp(string wabaId, [FromBody] WaSubscribeAppRequest request)
    {
        request.WabaId = wabaId;
        return Ok(await _service.SubscribeAppToWabaAsync(request));
    }

    /// <summary>List apps subscribed to a WABA's webhooks</summary>
    [HttpGet("waba/{wabaId}/subscribed-apps")]
    public async Task<IActionResult> GetSubscribedApps(string wabaId, [FromQuery] string accessToken) =>
        Ok(await _service.GetSubscribedAppsAsync(wabaId, accessToken));

    /// <summary>Unsubscribe your app from a WABA's webhooks</summary>
    [HttpDelete("waba/{wabaId}/subscribed-apps")]
    public async Task<IActionResult> UnsubscribeApp(string wabaId, [FromQuery] string accessToken) =>
        Ok(await _service.UnsubscribeAppFromWabaAsync(wabaId, accessToken));

    // ─── WABA settings ───────────────────────────────────────────────────────

    /// <summary>Turn marketing messages on Cloud API off (or back on) for a WABA</summary>
    [HttpPost("waba/{wabaId}/marketing-on-cloud-api")]
    public async Task<IActionResult> SetMarketingOnCloudApi(string wabaId, [FromBody] WaMarketingOnCloudApiRequest request)
    {
        request.WabaId = wabaId;
        return Ok(await _service.SetMarketingOnCloudApiAsync(request));
    }

    /// <summary>Enable template insights for a WABA (one-time, cannot be undone)</summary>
    [HttpPost("waba/{wabaId}/template-insights")]
    public async Task<IActionResult> EnableTemplateInsights(string wabaId, [FromQuery] string accessToken) =>
        Ok(await _service.EnableTemplateInsightsAsync(new WaWabaRequest { WabaId = wabaId, AccessToken = accessToken }));

    /// <summary>Add a phone number to a WABA</summary>
    [HttpPost("waba/{wabaId}/phone-numbers")]
    public async Task<IActionResult> AddPhoneNumber(string wabaId, [FromBody] WaAddPhoneNumberRequest request)
    {
        request.WabaId = wabaId;
        return Ok(await _service.AddPhoneNumberAsync(request));
    }

    // ─── Business portfolio ──────────────────────────────────────────────────

    /// <summary>List WABAs owned by a business portfolio</summary>
    [HttpGet("businesses/{businessId}/wabas/owned")]
    public async Task<IActionResult> GetOwnedWabas(string businessId, [FromQuery] string accessToken) =>
        Ok(await _service.GetOwnedWabasAsync(businessId, accessToken));

    /// <summary>List client WABAs shared with a business portfolio (Solution Partners / Tech Providers)</summary>
    [HttpGet("businesses/{businessId}/wabas/client")]
    public async Task<IActionResult> GetClientWabas(string businessId, [FromQuery] string accessToken) =>
        Ok(await _service.GetClientWabasAsync(businessId, accessToken));

    /// <summary>Exchange an Embedded Signup code for a business token (server-to-server, within 30 s)</summary>
    [HttpPost("embedded-signup/exchange-code")]
    public async Task<IActionResult> ExchangeCode([FromBody] WaExchangeCodeRequest request) =>
        Ok(await _service.ExchangeEmbeddedSignupCodeAsync(request));

    // ─── Phone number settings ───────────────────────────────────────────────

    /// <summary>Request a display name change for a phone number</summary>
    [HttpPost("phone-numbers/{phoneNumberId}/display-name")]
    public async Task<IActionResult> UpdateDisplayName(string phoneNumberId, [FromBody] WaDisplayNameRequest request)
    {
        request.PhoneNumberId = phoneNumberId;
        return Ok(await _service.UpdateDisplayNameAsync(request));
    }

    /// <summary>Get phone number settings (calling, storage, etc.)</summary>
    [HttpGet("phone-numbers/{phoneNumberId}/settings")]
    public async Task<IActionResult> GetSettings(string phoneNumberId, [FromQuery] string accessToken, [FromQuery] bool includeSipCredentials = false) =>
        Ok(await _service.GetPhoneSettingsAsync(phoneNumberId, accessToken, includeSipCredentials));

    /// <summary>Update phone number settings (e.g. storage_configuration for local storage)</summary>
    [HttpPost("phone-numbers/{phoneNumberId}/settings")]
    public async Task<IActionResult> UpdateSettings(string phoneNumberId, [FromBody] WaRawSettingsRequest request)
    {
        request.PhoneNumberId = phoneNumberId;
        return Ok(await _service.UpdatePhoneSettingsAsync(request));
    }

    /// <summary>Get welcome message, ice breakers and commands</summary>
    [HttpGet("phone-numbers/{phoneNumberId}/conversational-automation")]
    public async Task<IActionResult> GetConversationalAutomation(string phoneNumberId, [FromQuery] string accessToken) =>
        Ok(await _service.GetConversationalAutomationAsync(phoneNumberId, accessToken));

    /// <summary>Set welcome message, ice breakers (up to 4) and commands (up to 30)</summary>
    [HttpPost("phone-numbers/{phoneNumberId}/conversational-automation")]
    public async Task<IActionResult> UpdateConversationalAutomation(string phoneNumberId, [FromBody] WaConversationalAutomationRequest request)
    {
        request.PhoneNumberId = phoneNumberId;
        return Ok(await _service.UpdateConversationalAutomationAsync(request));
    }

    /// <summary>Get cart and catalog visibility settings</summary>
    [HttpGet("phone-numbers/{phoneNumberId}/commerce-settings")]
    public async Task<IActionResult> GetCommerceSettings(string phoneNumberId, [FromQuery] string accessToken) =>
        Ok(await _service.GetCommerceSettingsAsync(phoneNumberId, accessToken));

    /// <summary>Enable/disable the cart and catalog visibility</summary>
    [HttpPost("phone-numbers/{phoneNumberId}/commerce-settings")]
    public async Task<IActionResult> UpdateCommerceSettings(string phoneNumberId, [FromBody] WaCommerceSettingsRequest request)
    {
        request.PhoneNumberId = phoneNumberId;
        return Ok(await _service.UpdateCommerceSettingsAsync(request));
    }

    /// <summary>Get business compliance info (India)</summary>
    [HttpGet("phone-numbers/{phoneNumberId}/business-compliance")]
    public async Task<IActionResult> GetBusinessCompliance(string phoneNumberId, [FromQuery] string accessToken) =>
        Ok(await _service.GetBusinessComplianceAsync(phoneNumberId, accessToken));

    /// <summary>Submit business compliance info (India)</summary>
    [HttpPost("phone-numbers/{phoneNumberId}/business-compliance")]
    public async Task<IActionResult> UpdateBusinessCompliance(string phoneNumberId, [FromBody] WaRawSettingsRequest request)
    {
        request.PhoneNumberId = phoneNumberId;
        return Ok(await _service.UpdateBusinessComplianceAsync(request));
    }

    /// <summary>Get messaging health status of a WABA, phone number, template or business</summary>
    [HttpGet("health-status/{nodeId}")]
    public async Task<IActionResult> GetHealthStatus(string nodeId, [FromQuery] string accessToken) =>
        Ok(await _service.GetHealthStatusAsync(nodeId, accessToken));
}
