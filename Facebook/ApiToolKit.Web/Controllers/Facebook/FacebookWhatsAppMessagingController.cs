using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/whatsapp")]
[Produces("application/json")]
public class FacebookWhatsAppMessagingController : ControllerBase
{
    private readonly IFacebookWhatsAppMessagingService _service;

    public FacebookWhatsAppMessagingController(IFacebookWhatsAppMessagingService service) => _service = service;

    // ─── Messages ────────────────────────────────────────────────────────────

    /// <summary>Show a typing indicator (also marks the received message as read)</summary>
    [HttpPost("messages/typing-indicator")]
    public async Task<IActionResult> SendTypingIndicator([FromBody] WaTypingIndicatorRequest request) =>
        Ok(await _service.SendTypingIndicatorAsync(request));

    /// <summary>Send any message type (carousel, address, order, voice_call …) with a raw content object</summary>
    [HttpPost("messages/send")]
    public async Task<IActionResult> SendRawMessage([FromBody] WaSendRawMessageRequest request) =>
        Ok(await _service.SendRawMessageAsync(request));

    /// <summary>Send an interactive media carousel message (2–10 cards)</summary>
    [HttpPost("messages/interactive/carousel")]
    public async Task<IActionResult> SendCarousel([FromBody] WaSendCarouselRequest request) =>
        Ok(await _service.SendCarouselAsync(request));

    /// <summary>Send an interactive multi-product message (sections of catalog products)</summary>
    [HttpPost("messages/interactive/multi-product")]
    public async Task<IActionResult> SendMultiProduct([FromBody] WaSendMultiProductRequest request) =>
        Ok(await _service.SendMultiProductAsync(request));

    /// <summary>Send a marketing template through the Marketing Messages API (MM Lite)</summary>
    [HttpPost("messages/marketing")]
    public async Task<IActionResult> SendMarketingMessage([FromBody] WaSendMarketingMessageRequest request) =>
        Ok(await _service.SendMarketingMessageAsync(request));

    // ─── Media ───────────────────────────────────────────────────────────────

    /// <summary>Download a media file (resolves the media URL, then streams the file)</summary>
    [HttpGet("media/{mediaId}/download")]
    public async Task<IActionResult> DownloadMedia(string mediaId, [FromQuery] string accessToken)
    {
        var result = await _service.DownloadMediaAsync(mediaId, accessToken);
        if (!result.Success || result.Data == null) return Ok(result);
        return File(result.Data.Content, result.Data.ContentType, result.Data.FileName);
    }

    // ─── Block users ─────────────────────────────────────────────────────────

    /// <summary>Block WhatsApp users</summary>
    [HttpPost("block-users")]
    public async Task<IActionResult> BlockUsers([FromBody] WaUserListRequest request) =>
        Ok(await _service.BlockUsersAsync(request));

    /// <summary>Unblock WhatsApp users</summary>
    [HttpDelete("block-users")]
    public async Task<IActionResult> UnblockUsers([FromBody] WaUserListRequest request) =>
        Ok(await _service.UnblockUsersAsync(request));

    /// <summary>List blocked users</summary>
    [HttpGet("block-users")]
    public async Task<IActionResult> GetBlockedUsers([FromQuery] string phoneNumberId, [FromQuery] string accessToken,
        [FromQuery] int? limit, [FromQuery] string? after, [FromQuery] string? before) =>
        Ok(await _service.GetBlockedUsersAsync(phoneNumberId, accessToken, limit, after, before));

    // ─── Templates ───────────────────────────────────────────────────────────

    /// <summary>Search templates by name, status, category or language (paged)</summary>
    [HttpGet("templates/search")]
    public async Task<IActionResult> SearchTemplates([FromQuery] WaTemplateSearchQuery query) =>
        Ok(await _service.SearchTemplatesAsync(query));

    /// <summary>Browse Meta's pre-approved template library</summary>
    [HttpGet("templates/library")]
    public async Task<IActionResult> GetTemplateLibrary([FromQuery] WaTemplateLibraryQuery query) =>
        Ok(await _service.GetTemplateLibraryAsync(query));

    /// <summary>Create a template from the template library</summary>
    [HttpPost("templates/from-library")]
    public async Task<IActionResult> CreateTemplateFromLibrary([FromBody] WaCreateTemplateFromLibraryRequest request) =>
        Ok(await _service.CreateTemplateFromLibraryAsync(request));

    /// <summary>Delete every language version of a template by name</summary>
    [HttpDelete("templates/by-name")]
    public async Task<IActionResult> DeleteTemplatesByName([FromQuery] string wabaId, [FromQuery] string name, [FromQuery] string accessToken) =>
        Ok(await _service.DeleteTemplatesByNameAsync(wabaId, name, accessToken));

    /// <summary>Delete up to 100 templates by ID</summary>
    [HttpDelete("templates/bulk")]
    public async Task<IActionResult> BulkDeleteTemplates([FromQuery] string wabaId, [FromQuery] List<string> templateIds, [FromQuery] string accessToken) =>
        Ok(await _service.BulkDeleteTemplatesAsync(wabaId, templateIds, accessToken));

    /// <summary>Copy templates from one WABA to another</summary>
    [HttpPost("templates/migrate")]
    public async Task<IActionResult> MigrateTemplates([FromBody] WaMigrateTemplatesRequest request) =>
        Ok(await _service.MigrateTemplatesAsync(request));
}
