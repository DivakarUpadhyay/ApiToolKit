using ApiToolKit.Application.DTOs.Firebase.Fcm;
using ApiToolKit.Application.Interfaces.Services.Firebase;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Firebase;

[ApiController]
[Route("api/firebase/fcm")]
public class FcmController : ControllerBase
{
    private readonly IFcmService _fcmService;

    public FcmController(IFcmService fcmService)
    {
        _fcmService = fcmService;
    }

    private string GetAccessToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        return authHeader.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();
    }

    /// <summary>Send a push notification to a specific device token using FCM v1.</summary>
    [HttpPost("send/token")]
    public async Task<IActionResult> SendToToken([FromBody] SendToTokenRequest request)
    {
        var result = await _fcmService.SendToTokenAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Send a push notification to a topic using FCM v1.</summary>
    [HttpPost("send/topic")]
    public async Task<IActionResult> SendToTopic([FromBody] SendToTopicRequest request)
    {
        var result = await _fcmService.SendToTopicAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Send a push notification based on a topic condition using FCM v1.</summary>
    [HttpPost("send/condition")]
    public async Task<IActionResult> SendToCondition([FromBody] SendToConditionRequest request)
    {
        var result = await _fcmService.SendToConditionAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Send push notifications to multiple device tokens (up to 500) using FCM v1.</summary>
    [HttpPost("send/multicast")]
    public async Task<IActionResult> SendMulticast([FromBody] MulticastRequest request)
    {
        var result = await _fcmService.SendMulticastAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Subscribe device tokens to a FCM topic.</summary>
    [HttpPost("topics/subscribe")]
    public async Task<IActionResult> SubscribeToTopic([FromBody] SubscribeToTopicRequest request)
    {
        var result = await _fcmService.SubscribeToTopicAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Unsubscribe device tokens from a FCM topic.</summary>
    [HttpPost("topics/unsubscribe")]
    public async Task<IActionResult> UnsubscribeFromTopic([FromBody] UnsubscribeFromTopicRequest request)
    {
        var result = await _fcmService.UnsubscribeFromTopicAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
