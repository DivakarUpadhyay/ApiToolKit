using ApiToolKit.Application.DTOs.YouTube.Subscription;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.YouTube;

[ApiController]
[Route("api/youtube/subscriptions")]
[Produces("application/json")]
public class YouTubeSubscriptionController : ControllerBase
{
    private readonly IYouTubeSubscriptionService _subscriptionService;

    public YouTubeSubscriptionController(IYouTubeSubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    private string GetAccessToken() => Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    /// <summary>List subscriptions for the authenticated user or a channel</summary>
    [HttpGet]
    public async Task<IActionResult> ListSubscriptions([FromQuery] ListSubscriptionsRequest request)
    {
        var result = await _subscriptionService.ListSubscriptionsAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Subscribe to a channel</summary>
    [HttpPost]
    public async Task<IActionResult> Subscribe([FromBody] SubscribeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _subscriptionService.SubscribeAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a subscription</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubscription([FromRoute] string id)
    {
        var request = new DeleteSubscriptionRequest { SubscriptionId = id };
        var result = await _subscriptionService.DeleteSubscriptionAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
