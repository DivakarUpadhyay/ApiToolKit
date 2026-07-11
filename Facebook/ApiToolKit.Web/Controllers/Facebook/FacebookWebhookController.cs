using ApiToolKit.Application.DTOs.Facebook.Webhook;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/webhook")]
[Produces("application/json")]
public class FacebookWebhookController : ControllerBase
{
    private readonly IFacebookWebhookService _webhookService;

    public FacebookWebhookController(IFacebookWebhookService webhookService)
    {
        _webhookService = webhookService;
    }

    /// <summary>Subscribe to Facebook webhook events</summary>
    [HttpPost("subscribe")]
    public async Task<IActionResult> SubscribeWebhookAsync([FromBody] SubscribeWebhookRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _webhookService.SubscribeWebhookAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get current webhook subscriptions for an app</summary>
    [HttpPost("subscriptions")]
    public async Task<IActionResult> GetWebhookSubscriptionsAsync([FromBody] GetWebhookSubscriptionsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _webhookService.GetWebhookSubscriptionsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a webhook subscription</summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteWebhookAsync([FromBody] DeleteWebhookRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _webhookService.DeleteWebhookAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Verify webhook challenge from Facebook (GET verification)</summary>
    [HttpGet("verify")]
    public async Task<IActionResult> VerifyWebhookAsync(
        [FromQuery(Name = "hub.mode")] string? mode,
        [FromQuery(Name = "hub.verify_token")] string? token,
        [FromQuery(Name = "hub.challenge")] string? challenge)
    {
        var request = new WebhookVerificationRequest
        {
            Mode = mode,
            Token = token,
            Challenge = challenge
        };
        var result = await _webhookService.VerifyWebhookAsync(request);
        if (result.Success && result.Data != null)
            return Ok(result.Data);
        return Unauthorized(result);
    }

    /// <summary>Receive and process incoming webhook payloads from Facebook</summary>
    [HttpPost("payload")]
    public async Task<IActionResult> ProcessWebhookPayloadAsync([FromBody] WebhookPayload payload)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _webhookService.ProcessWebhookPayloadAsync(payload);
        return Ok(result);
    }
}
