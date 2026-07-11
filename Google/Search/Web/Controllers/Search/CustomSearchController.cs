using ApiToolKit.Application.DTOs.Search.CustomSearch;
using ApiToolKit.Application.Interfaces.Services.Search;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Search;

[ApiController]
[Route("api/search/custom")]
public class CustomSearchController : ControllerBase
{
    private readonly ICustomSearchService _customSearchService;

    public CustomSearchController(ICustomSearchService customSearchService)
    {
        _customSearchService = customSearchService;
    }

    /// <summary>Perform a web search using the Google Custom Search API.</summary>
    /// <remarks>
    /// Returns up to 10 results per page. Use the Start parameter for pagination.
    /// The Cx (Search Engine ID) parameter is optional — if omitted, the configured default is used.
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] CustomSearchRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _customSearchService.SearchAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Perform an image search using the Google Custom Search API.</summary>
    /// <remarks>
    /// Searches specifically for images. Supports filtering by ImageSize, ImageType, ImageColorType, and ImageDominantColor.
    /// </remarks>
    [HttpGet("images")]
    public async Task<IActionResult> ImageSearch([FromQuery] ImageSearchRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _customSearchService.ImageSearchAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
