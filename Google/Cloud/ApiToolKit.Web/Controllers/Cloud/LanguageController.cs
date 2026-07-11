using ApiToolKit.Application.DTOs.Cloud.NaturalLanguage;
using ApiToolKit.Application.Interfaces.Services.Cloud;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Cloud;

/// <summary>
/// Google Cloud Natural Language API endpoints for text analysis.
/// </summary>
[ApiController]
[Route("api/cloud/language")]
public class LanguageController : ControllerBase
{
    private readonly ILanguageService _languageService;

    public LanguageController(ILanguageService languageService)
    {
        _languageService = languageService;
    }

    /// <summary>
    /// Identifies named entities (people, places, organizations, etc.) in text.
    /// </summary>
    [HttpPost("entities")]
    public async Task<IActionResult> AnalyzeEntities([FromBody] AnalyzeEntitiesRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _languageService.AnalyzeEntitiesAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Analyzes the overall sentiment (positive, negative, neutral) of a block of text.
    /// </summary>
    [HttpPost("sentiment")]
    public async Task<IActionResult> AnalyzeSentiment([FromBody] AnalyzeSentimentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _languageService.AnalyzeSentimentAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Analyzes the grammatical structure (tokens, part-of-speech, dependency trees) of text.
    /// </summary>
    [HttpPost("syntax")]
    public async Task<IActionResult> AnalyzeSyntax([FromBody] AnalyzeSyntaxRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _languageService.AnalyzeSyntaxAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Classifies text into predefined content categories from the Google taxonomy.
    /// </summary>
    [HttpPost("classify")]
    public async Task<IActionResult> ClassifyText([FromBody] ClassifyTextRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _languageService.ClassifyTextAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Moderates text for harmful content categories (toxicity, violence, etc.).
    /// </summary>
    [HttpPost("moderate")]
    public async Task<IActionResult> ModerateText([FromBody] ModerateTextRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _languageService.ModerateTextAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Performs multiple Natural Language analysis tasks (entities, sentiment, syntax, classification) in a single call.
    /// </summary>
    [HttpPost("annotate")]
    public async Task<IActionResult> AnnotateText([FromBody] AnnotateTextRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _languageService.AnnotateTextAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }
}
