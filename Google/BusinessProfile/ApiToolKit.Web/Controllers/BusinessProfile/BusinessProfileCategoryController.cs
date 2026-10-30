using ApiToolKit.Application.DTOs.BusinessProfile.Category;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BusinessProfile;

/// <summary>
/// Business categories.
/// </summary>
[ApiController]
[Route("api/businessprofile/categories")]
[Produces("application/json")]
public class BusinessProfileCategoryController : ControllerBase
{
    private readonly IBusinessProfileCategoryService _categoryService;

    public BusinessProfileCategoryController(IBusinessProfileCategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>List business categories for a region and language.</summary>
    [HttpGet]
    public async Task<IActionResult> ListCategoriesAsync([FromQuery] string accountKey, [FromQuery] string regionCode, [FromQuery] string languageCode, [FromQuery] string? filter = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(regionCode)) return BadRequest("regionCode is required.");
        if (string.IsNullOrEmpty(languageCode)) return BadRequest("languageCode is required.");
        var request = new ListCategoriesRequest { AccountKey = accountKey, RegionCode = regionCode, LanguageCode = languageCode, Filter = filter, PageSize = pageSize, PageToken = pageToken };
        var result = await _categoryService.ListCategoriesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get several categories by name.</summary>
    [HttpGet("batch")]
    public async Task<IActionResult> BatchGetCategoriesAsync([FromQuery] string accountKey, [FromQuery] List<string> names, [FromQuery] string languageCode, [FromQuery] string? regionCode = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (names == null || names.Count == 0) return BadRequest("names is required.");
        if (string.IsNullOrEmpty(languageCode)) return BadRequest("languageCode is required.");
        var request = new BatchGetCategoriesRequest { AccountKey = accountKey, Names = names, LanguageCode = languageCode, RegionCode = regionCode };
        var result = await _categoryService.BatchGetCategoriesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List attributes available for a category or location.</summary>
    [HttpGet("attributes")]
    public async Task<IActionResult> ListAttributeMetadataAsync([FromQuery] string accountKey, [FromQuery] string? categoryName = null, [FromQuery] string? parent = null, [FromQuery] string? regionCode = null, [FromQuery] string? languageCode = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAttributeMetadataRequest { AccountKey = accountKey, CategoryName = categoryName, Parent = parent, RegionCode = regionCode, LanguageCode = languageCode, PageSize = pageSize, PageToken = pageToken };
        var result = await _categoryService.ListAttributeMetadataAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a business chain (brand).</summary>
    [HttpGet("chains/{chainId}")]
    public async Task<IActionResult> GetChainAsync([FromQuery] string accountKey, [FromRoute] string chainId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetChainRequest { AccountKey = accountKey, ChainId = chainId };
        var result = await _categoryService.GetChainAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Search business chains by name.</summary>
    [HttpGet("chains")]
    public async Task<IActionResult> SearchChainsAsync([FromQuery] string accountKey, [FromQuery] string chainName, [FromQuery] int? pageSize = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(chainName)) return BadRequest("chainName is required.");
        var request = new SearchChainsRequest { AccountKey = accountKey, ChainName = chainName, PageSize = pageSize };
        var result = await _categoryService.SearchChainsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
