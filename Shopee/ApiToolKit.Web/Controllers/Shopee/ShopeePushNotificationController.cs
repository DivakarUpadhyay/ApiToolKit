using ApiToolKit.Application.DTOs.Shopee.PushNotification;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/push")]
[Produces("application/json")]
public class ShopeePushNotificationController : ControllerBase
{
    private readonly IShopeePushNotificationService _svc;

    public ShopeePushNotificationController(IShopeePushNotificationService svc)
    {
        _svc = svc;
    }

    /// <summary>Set the push notification configuration for the app</summary>
    [HttpPost("config")]
    public async Task<IActionResult> SetAppPushConfig([FromBody] SetAppPushConfigRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.SetAppPushConfigAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the current push notification configuration for the app</summary>
    [HttpGet("config")]
    public async Task<IActionResult> GetAppPushConfig([FromQuery] GetAppPushConfigRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetAppPushConfigAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
