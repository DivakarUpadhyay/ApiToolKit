using ApiToolKit.Application.DTOs.Merchant.NotificationsNotificationsubscription;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// notificationsubscriptions (v1)
/// </summary>
[ApiController]
[Route("api/merchant/notifications")]
[Produces("application/json")]
public class MerchantNotificationsNotificationsubscriptionController : ControllerBase
{
    private readonly IMerchantNotificationsNotificationsubscriptionService _notificationsNotificationsubscriptionService;

    public MerchantNotificationsNotificationsubscriptionController(IMerchantNotificationsNotificationsubscriptionService notificationsNotificationsubscriptionService)
    {
        _notificationsNotificationsubscriptionService = notificationsNotificationsubscriptionService;
    }

    /// <summary>Updates an existing notification subscription for a merchant.</summary>
    [HttpPatch("accounts/{accountId}/notificationsubscriptions/{notificationsubscriptionId}")]
    public async Task<IActionResult> UpdateNotificationsubscriptionAsync([FromRoute] string accountId, [FromRoute] string notificationsubscriptionId, [FromBody] UpdateNotificationsubscriptionRequest request)
    {
        request.AccountId = accountId;
        request.NotificationsubscriptionId = notificationsubscriptionId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _notificationsNotificationsubscriptionService.UpdateNotificationsubscriptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a notification subscription for a business.</summary>
    [HttpPost("accounts/{accountId}/notificationsubscriptions")]
    public async Task<IActionResult> CreateNotificationsubscriptionAsync([FromRoute] string accountId, [FromBody] CreateNotificationsubscriptionRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _notificationsNotificationsubscriptionService.CreateNotificationsubscriptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a notification subscription for a merchant.</summary>
    [HttpDelete("accounts/{accountId}/notificationsubscriptions/{notificationsubscriptionId}")]
    public async Task<IActionResult> DeleteNotificationsubscriptionAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string notificationsubscriptionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteNotificationsubscriptionRequest { AccountKey = accountKey, AccountId = accountId, NotificationsubscriptionId = notificationsubscriptionId };
        var result = await _notificationsNotificationsubscriptionService.DeleteNotificationsubscriptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets notification subscriptions for an account.</summary>
    [HttpGet("accounts/{accountId}/notificationsubscriptions/{notificationsubscriptionId}")]
    public async Task<IActionResult> GetNotificationsubscriptionAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string notificationsubscriptionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetNotificationsubscriptionRequest { AccountKey = accountKey, AccountId = accountId, NotificationsubscriptionId = notificationsubscriptionId };
        var result = await _notificationsNotificationsubscriptionService.GetNotificationsubscriptionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets all the notification subscriptions for a merchant.</summary>
    [HttpGet("accounts/{accountId}/notificationsubscriptions")]
    public async Task<IActionResult> ListNotificationsubscriptionsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListNotificationsubscriptionsRequest { AccountKey = accountKey, AccountId = accountId, PageToken = pageToken, PageSize = pageSize };
        var result = await _notificationsNotificationsubscriptionService.ListNotificationsubscriptionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
