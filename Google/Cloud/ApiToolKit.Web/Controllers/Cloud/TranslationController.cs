using ApiToolKit.Application.DTOs.Cloud.Translation;
using ApiToolKit.Application.Interfaces.Services.Cloud;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Cloud;

/// <summary>
/// Google Cloud Translation API v3 endpoints for text translation, language detection, and glossary management.
/// </summary>
[ApiController]
[Route("api/cloud/translation")]
public class TranslationController : ControllerBase
{
    private readonly ITranslationService _translationService;

    public TranslationController(ITranslationService translationService)
    {
        _translationService = translationService;
    }

    /// <summary>
    /// Translates one or more texts into the specified target language.
    /// </summary>
    [HttpPost("translate")]
    public async Task<IActionResult> TranslateText([FromBody] TranslateTextRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _translationService.TranslateTextAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Detects the language of a given text.
    /// </summary>
    [HttpPost("detect")]
    public async Task<IActionResult> DetectLanguage([FromBody] DetectLanguageRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _translationService.DetectLanguageAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Returns a list of languages supported by the Translation API for a given project.
    /// </summary>
    [HttpGet("languages")]
    public async Task<IActionResult> GetSupportedLanguages([FromQuery] string projectId, [FromQuery] string displayLanguageCode = "en", [FromQuery] string? model = null)
    {
        if (string.IsNullOrEmpty(projectId))
            return BadRequest(new { Message = "projectId query parameter is required." });

        var request = new GetSupportedLanguagesRequest
        {
            ProjectId = projectId,
            DisplayLanguageCode = displayLanguageCode,
            Model = model
        };

        var result = await _translationService.GetSupportedLanguagesAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Romanizes text from a non-Latin script to Latin characters.
    /// </summary>
    [HttpPost("romanize")]
    public async Task<IActionResult> RomanizeText([FromBody] RomanizeTextRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _translationService.RomanizeTextAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Starts an asynchronous batch translation job for large volumes of text stored in Google Cloud Storage.
    /// Returns a long-running operation name.
    /// </summary>
    [HttpPost("batch-translate")]
    public async Task<IActionResult> BatchTranslateText([FromBody] BatchTranslateTextRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _translationService.BatchTranslateTextAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Creates a custom glossary for terminology-aware translation.
    /// </summary>
    [HttpPost("glossaries")]
    public async Task<IActionResult> CreateGlossary([FromBody] CreateGlossaryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _translationService.CreateGlossaryAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Lists all glossaries for a given project.
    /// </summary>
    [HttpGet("glossaries")]
    public async Task<IActionResult> ListGlossaries([FromQuery] string projectId, [FromQuery] string? accessToken)
    {
        if (string.IsNullOrEmpty(projectId))
            return BadRequest(new { Message = "projectId query parameter is required." });

        var result = await _translationService.ListGlossariesAsync(projectId, accessToken ?? string.Empty);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }

    /// <summary>
    /// Deletes a glossary by its resource name.
    /// </summary>
    [HttpDelete("glossaries/{name}")]
    public async Task<IActionResult> DeleteGlossary([FromRoute] string name, [FromQuery] string projectId, [FromQuery] string location = "us-central1")
    {
        if (string.IsNullOrEmpty(projectId))
            return BadRequest(new { Message = "projectId query parameter is required." });

        var request = new DeleteGlossaryRequest
        {
            ProjectId = projectId,
            GlossaryName = name,
            Location = location
        };

        var result = await _translationService.DeleteGlossaryAsync(request);
        if (!result.Success) return BadRequest(new { result.Message });
        return Ok(result.Data);
    }
}
