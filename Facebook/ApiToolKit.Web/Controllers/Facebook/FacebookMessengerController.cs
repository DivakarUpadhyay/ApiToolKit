using ApiToolKit.Application.DTOs.Facebook.Messenger;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/messenger")]
[Produces("application/json")]
public class FacebookMessengerController : ControllerBase
{
    private readonly IFacebookMessengerService _messengerService;

    public FacebookMessengerController(IFacebookMessengerService messengerService)
    {
        _messengerService = messengerService;
    }

    /// <summary>Send a message to a user via Messenger</summary>
    [HttpPost("send")]
    public async Task<IActionResult> SendMessageAsync([FromBody] SendMessageRequest request, [FromQuery] string pageAccessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _messengerService.SendMessageAsync(request, pageAccessToken);
        return Ok(result);
    }

    /// <summary>Get conversations for a page</summary>
    [HttpPost("conversations")]
    public async Task<IActionResult> GetConversationsAsync([FromBody] GetConversationsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _messengerService.GetConversationsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get messages in a conversation</summary>
    [HttpPost("messages")]
    public async Task<IActionResult> GetMessagesAsync([FromBody] GetMessagesRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _messengerService.GetMessagesAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get a Messenger user's profile by PSID</summary>
    [HttpPost("user/profile")]
    public async Task<IActionResult> GetUserProfileAsync([FromBody] GetUserProfileRequest request, [FromQuery] string pageAccessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _messengerService.GetUserProfileAsync(request, pageAccessToken);
        return Ok(result);
    }

    /// <summary>Mark a conversation as seen</summary>
    [HttpPost("mark-seen")]
    public async Task<IActionResult> MarkSeenAsync([FromBody] MarkSeenRequest request, [FromQuery] string pageAccessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _messengerService.MarkSeenAsync(request, pageAccessToken);
        return Ok(result);
    }

    /// <summary>Send a typing indicator to a conversation</summary>
    [HttpPost("typing")]
    public async Task<IActionResult> SendTypingIndicatorAsync([FromBody] TypingOnRequest request, [FromQuery] string pageAccessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _messengerService.SendTypingIndicatorAsync(request, pageAccessToken);
        return Ok(result);
    }

    /// <summary>Subscribe the app to a page for Messenger events</summary>
    [HttpPost("subscribe")]
    public async Task<IActionResult> SubscribeAppToPageAsync([FromBody] SubscribeAppRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _messengerService.SubscribeAppToPageAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get current webhook subscriptions for a page</summary>
    [HttpGet("page/{pageId}/subscriptions")]
    public async Task<IActionResult> GetPageSubscriptionsAsync(string pageId, [FromQuery] string accessToken)
    {
        var result = await _messengerService.GetPageSubscriptionsAsync(pageId, accessToken);
        return Ok(result);
    }

    /// <summary>Set the Messenger profile (get started, greeting, persistent menu)</summary>
    [HttpPost("page/{pageId}/profile")]
    public async Task<IActionResult> SetMessengerProfileAsync(string pageId, [FromBody] object profileSettings, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _messengerService.SetMessengerProfileAsync(pageId, profileSettings, accessToken);
        return Ok(result);
    }
}
