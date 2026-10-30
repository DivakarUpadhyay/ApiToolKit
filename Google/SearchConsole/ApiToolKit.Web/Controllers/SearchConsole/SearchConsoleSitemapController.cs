using ApiToolKit.Application.DTOs.SearchConsole.Sitemap;
using ApiToolKit.Application.Interfaces.Services.SearchConsole;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.SearchConsole;

/// <summary>
/// Sitemaps submitted for a property.
/// </summary>
[ApiController]
[Route("api/searchconsole/sitemaps")]
[Produces("application/json")]
public class SearchConsoleSitemapController : ControllerBase
{
    private readonly ISearchConsoleSitemapService _sitemapService;

    public SearchConsoleSitemapController(ISearchConsoleSitemapService sitemapService)
    {
        _sitemapService = sitemapService;
    }

    /// <summary>List sitemaps submitted for a property.</summary>
    [HttpGet]
    public async Task<IActionResult> ListSitemapsAsync([FromQuery] string accountKey, [FromQuery] string siteUrl, [FromQuery] string? sitemapIndex = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(siteUrl)) return BadRequest("siteUrl is required.");
        var request = new ListSitemapsRequest { AccountKey = accountKey, SiteUrl = siteUrl, SitemapIndex = sitemapIndex };
        var result = await _sitemapService.ListSitemapsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get status, errors and warnings for one sitemap.</summary>
    [HttpGet("detail")]
    public async Task<IActionResult> GetSitemapAsync([FromQuery] string accountKey, [FromQuery] string siteUrl, [FromQuery] string feedpath)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(siteUrl)) return BadRequest("siteUrl is required.");
        if (string.IsNullOrEmpty(feedpath)) return BadRequest("feedpath is required.");
        var request = new GetSitemapRequest { AccountKey = accountKey, SiteUrl = siteUrl, Feedpath = feedpath };
        var result = await _sitemapService.GetSitemapAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Submit a sitemap for a property.</summary>
    [HttpPost]
    public async Task<IActionResult> SubmitSitemapAsync([FromBody] SubmitSitemapRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _sitemapService.SubmitSitemapAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a sitemap from a property.</summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteSitemapAsync([FromQuery] string accountKey, [FromQuery] string siteUrl, [FromQuery] string feedpath)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(siteUrl)) return BadRequest("siteUrl is required.");
        if (string.IsNullOrEmpty(feedpath)) return BadRequest("feedpath is required.");
        var request = new DeleteSitemapRequest { AccountKey = accountKey, SiteUrl = siteUrl, Feedpath = feedpath };
        var result = await _sitemapService.DeleteSitemapAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
