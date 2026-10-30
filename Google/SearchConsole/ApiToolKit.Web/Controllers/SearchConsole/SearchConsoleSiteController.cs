using ApiToolKit.Application.DTOs.SearchConsole.Site;
using ApiToolKit.Application.Interfaces.Services.SearchConsole;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.SearchConsole;

/// <summary>
/// Search Console properties (sites).
/// </summary>
[ApiController]
[Route("api/searchconsole/sites")]
[Produces("application/json")]
public class SearchConsoleSiteController : ControllerBase
{
    private readonly ISearchConsoleSiteService _siteService;

    public SearchConsoleSiteController(ISearchConsoleSiteService siteService)
    {
        _siteService = siteService;
    }

    /// <summary>List all Search Console properties the account can access.</summary>
    [HttpGet]
    public async Task<IActionResult> ListSitesAsync([FromQuery] string accountKey)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListSitesRequest { AccountKey = accountKey };
        var result = await _siteService.ListSitesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get permission level for one property.</summary>
    [HttpGet("detail")]
    public async Task<IActionResult> GetSiteAsync([FromQuery] string accountKey, [FromQuery] string siteUrl)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(siteUrl)) return BadRequest("siteUrl is required.");
        var request = new GetSiteRequest { AccountKey = accountKey, SiteUrl = siteUrl };
        var result = await _siteService.GetSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add a property to Search Console (ownership must then be verified).</summary>
    [HttpPost]
    public async Task<IActionResult> AddSiteAsync([FromBody] AddSiteRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _siteService.AddSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove a property from Search Console.</summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteSiteAsync([FromQuery] string accountKey, [FromQuery] string siteUrl)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(siteUrl)) return BadRequest("siteUrl is required.");
        var request = new DeleteSiteRequest { AccountKey = accountKey, SiteUrl = siteUrl };
        var result = await _siteService.DeleteSiteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
