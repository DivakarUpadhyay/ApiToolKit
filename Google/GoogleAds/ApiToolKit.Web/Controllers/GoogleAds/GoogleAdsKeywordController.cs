using ApiToolKit.Application.DTOs.GoogleAds.Keyword;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.GoogleAds;

[ApiController]
[Route("api/googleads/keywords")]
[Produces("application/json")]
public class GoogleAdsKeywordController : ControllerBase
{
    private readonly IGoogleAdsKeywordService _keywordService;

    public GoogleAdsKeywordController(IGoogleAdsKeywordService keywordService)
    {
        _keywordService = keywordService;
    }

    /// <summary>List keywords for a customer, optionally filtered by ad group</summary>
    [HttpGet]
    public async Task<IActionResult> ListKeywords([FromQuery] string customerId, [FromQuery] string? adGroupResourceName = null, [FromQuery] int pageSize = 50)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId is required.");
        var request = new ListKeywordsRequest { CustomerId = customerId, AdGroupResourceName = adGroupResourceName, PageSize = pageSize };
        var result = await _keywordService.ListKeywordsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new keyword</summary>
    [HttpPost]
    public async Task<IActionResult> CreateKeyword([FromBody] CreateKeywordRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _keywordService.CreateKeywordAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing keyword</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateKeyword([FromBody] UpdateKeywordRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _keywordService.UpdateKeywordAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove (delete) a keyword</summary>
    [HttpDelete("{customerId}/{*resourceName}")]
    public async Task<IActionResult> RemoveKeyword(string customerId, string resourceName)
    {
        var result = await _keywordService.RemoveKeywordAsync(customerId, resourceName);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
