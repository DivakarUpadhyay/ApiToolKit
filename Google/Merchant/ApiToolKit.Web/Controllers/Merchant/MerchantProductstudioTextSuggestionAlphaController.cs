using ApiToolKit.Application.DTOs.Merchant.ProductstudioTextSuggestionAlpha;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// textSuggestions (Alpha)
/// </summary>
[ApiController]
[Route("api/merchant/productstudio-v1alpha")]
[Produces("application/json")]
public class MerchantProductstudioTextSuggestionAlphaController : ControllerBase
{
    private readonly IMerchantProductstudioTextSuggestionAlphaService _productstudioTextSuggestionAlphaService;

    public MerchantProductstudioTextSuggestionAlphaController(IMerchantProductstudioTextSuggestionAlphaService productstudioTextSuggestionAlphaService)
    {
        _productstudioTextSuggestionAlphaService = productstudioTextSuggestionAlphaService;
    }

    /// <summary>GenerateProductTextSuggestions generates a set of candidate text completions (e.g., product titles, descriptions) based on provided product information.</summary>
    [HttpPost("accounts/{accountId}/generateProductTextSuggestions")]
    public async Task<IActionResult> GenerateProductTextSuggestionsAsync([FromRoute] string accountId, [FromBody] GenerateProductTextSuggestionsRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productstudioTextSuggestionAlphaService.GenerateProductTextSuggestionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
