using ApiToolKit.Application.DTOs.Facebook.Pixel;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;
namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/pixel")]
[Produces("application/json")]
public class FacebookPixelController : ControllerBase {
    private readonly IFacebookPixelService _service;
    public FacebookPixelController(IFacebookPixelService service) => _service = service;

    /// <summary>Create a new Meta Pixel for the specified ad account</summary>
    [HttpPost("account/{adAccountId}")]
    public async Task<IActionResult> CreatePixel(string adAccountId, [FromBody] CreatePixelRequest request, [FromQuery] string accessToken) {
        var result = await _service.CreatePixelAsync(adAccountId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Get all Meta Pixels for the specified ad account</summary>
    [HttpGet("account/{adAccountId}")]
    public async Task<IActionResult> GetPixels(string adAccountId, [FromQuery] string accessToken) {
        var result = await _service.GetPixelsAsync(adAccountId, accessToken);
        return Ok(result);
    }

    /// <summary>Get details of a specific Meta Pixel by ID</summary>
    [HttpGet("{pixelId}")]
    public async Task<IActionResult> GetPixel(string pixelId, [FromQuery] string accessToken) {
        var result = await _service.GetPixelAsync(pixelId, accessToken);
        return Ok(result);
    }

    /// <summary>Update the name of a specific Meta Pixel</summary>
    [HttpPut("{pixelId}")]
    public async Task<IActionResult> UpdatePixel(string pixelId, [FromBody] UpdatePixelRequest request, [FromQuery] string accessToken) {
        var result = await _service.UpdatePixelAsync(pixelId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Get firing statistics for a specific Meta Pixel</summary>
    [HttpGet("{pixelId}/stats")]
    public async Task<IActionResult> GetPixelStats(string pixelId, [FromQuery] string accessToken, [FromQuery] string? startTime = null, [FromQuery] string? endTime = null) {
        var result = await _service.GetPixelStatsAsync(pixelId, accessToken, startTime, endTime);
        return Ok(result);
    }

    /// <summary>Get custom audiences associated with a specific Meta Pixel</summary>
    [HttpGet("{pixelId}/audiences")]
    public async Task<IActionResult> GetPixelAudiences(string pixelId, [FromQuery] string accessToken) {
        var result = await _service.GetPixelAudiencesAsync(pixelId, accessToken);
        return Ok(result);
    }

    /// <summary>Share a Meta Pixel with another ad account</summary>
    [HttpPost("{pixelId}/share")]
    public async Task<IActionResult> SharePixel(string pixelId, [FromBody] SharePixelRequest request, [FromQuery] string accessToken) {
        var result = await _service.SharePixelAsync(pixelId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Get diagnostic information for a specific Meta Pixel</summary>
    [HttpGet("{pixelId}/diagnostics")]
    public async Task<IActionResult> GetPixelDiagnostics(string pixelId, [FromQuery] string accessToken) {
        var result = await _service.GetPixelDiagnosticsAsync(pixelId, accessToken);
        return Ok(result);
    }
}
