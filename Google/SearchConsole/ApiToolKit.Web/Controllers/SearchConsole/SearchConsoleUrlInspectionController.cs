using ApiToolKit.Application.DTOs.SearchConsole.UrlInspection;
using ApiToolKit.Application.Interfaces.Services.SearchConsole;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.SearchConsole;

/// <summary>
/// URL Inspection: index status, crawl and mobile usability for a URL.
/// </summary>
[ApiController]
[Route("api/searchconsole/url-inspection")]
[Produces("application/json")]
public class SearchConsoleUrlInspectionController : ControllerBase
{
    private readonly ISearchConsoleUrlInspectionService _urlInspectionService;

    public SearchConsoleUrlInspectionController(ISearchConsoleUrlInspectionService urlInspectionService)
    {
        _urlInspectionService = urlInspectionService;
    }

    /// <summary>Inspect how Google crawled and indexed a URL.</summary>
    [HttpPost]
    public async Task<IActionResult> InspectUrlAsync([FromBody] InspectUrlRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _urlInspectionService.InspectUrlAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
