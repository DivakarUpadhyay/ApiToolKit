using ApiToolKit.Application.DTOs.SearchConsole.Indexing;
using ApiToolKit.Application.Interfaces.Services.SearchConsole;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.SearchConsole;

/// <summary>
/// Indexing API. Google only supports pages with JobPosting or BroadcastEvent (livestream) structured data.
/// </summary>
[ApiController]
[Route("api/indexing")]
[Produces("application/json")]
public class SearchConsoleIndexingController : ControllerBase
{
    private readonly ISearchConsoleIndexingService _indexingService;

    public SearchConsoleIndexingController(ISearchConsoleIndexingService indexingService)
    {
        _indexingService = indexingService;
    }

    /// <summary>Tell Google a job posting or livestream URL was updated or deleted.</summary>
    [HttpPost("publish")]
    public async Task<IActionResult> PublishUrlNotificationAsync([FromBody] PublishUrlNotificationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _indexingService.PublishUrlNotificationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the latest notification Google received for a URL.</summary>
    [HttpGet("metadata")]
    public async Task<IActionResult> GetUrlNotificationMetadataAsync([FromQuery] string accountKey, [FromQuery] string url)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(url)) return BadRequest("url is required.");
        var request = new GetUrlNotificationMetadataRequest { AccountKey = accountKey, Url = url };
        var result = await _indexingService.GetUrlNotificationMetadataAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
