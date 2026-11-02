using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/whatsapp/calling")]
[Produces("application/json")]
public class FacebookWhatsAppCallingController : ControllerBase
{
    private readonly IFacebookWhatsAppCallingService _service;

    public FacebookWhatsAppCallingController(IFacebookWhatsAppCallingService service) => _service = service;

    /// <summary>Get calling settings (status, call hours, callback permission, SIP)</summary>
    [HttpGet("settings")]
    public async Task<IActionResult> GetSettings([FromQuery] string phoneNumberId, [FromQuery] string accessToken, [FromQuery] bool includeSipCredentials = false) =>
        Ok(await _service.GetCallSettingsAsync(phoneNumberId, accessToken, includeSipCredentials));

    /// <summary>Update calling settings — body Settings = { "calling": { ... } }</summary>
    [HttpPost("settings")]
    public async Task<IActionResult> UpdateSettings([FromBody] WaRawSettingsRequest request) =>
        Ok(await _service.UpdateCallSettingsAsync(request));

    /// <summary>Call action: connect, pre-accept, accept, reject or terminate</summary>
    [HttpPost("calls/{action}")]
    public async Task<IActionResult> CallAction(string action, [FromBody] WaCallActionRequest request) =>
        Ok(await _service.CallActionAsync(action, request));

    /// <summary>Get call permission state for a user (by phone number or BSUID)</summary>
    [HttpGet("permissions")]
    public async Task<IActionResult> GetPermissions([FromQuery] string phoneNumberId, [FromQuery] string accessToken,
        [FromQuery] string? userWaId, [FromQuery] string? recipient) =>
        Ok(await _service.GetCallPermissionsAsync(phoneNumberId, accessToken, userWaId, recipient));

    /// <summary>Send a call permission request message</summary>
    [HttpPost("permission-request")]
    public async Task<IActionResult> SendPermissionRequest([FromBody] WaCallPermissionRequestMessage request) =>
        Ok(await _service.SendCallPermissionRequestAsync(request));

    /// <summary>Send an interactive voice call button message</summary>
    [HttpPost("call-button")]
    public async Task<IActionResult> SendVoiceCallButton([FromBody] WaVoiceCallButtonMessage request) =>
        Ok(await _service.SendVoiceCallButtonAsync(request));
}
