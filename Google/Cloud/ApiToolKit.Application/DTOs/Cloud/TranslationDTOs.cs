using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Cloud.Translation;

public class GlossaryConfig
{
    public string? Glossary { get; set; }
    public bool IgnoreCase { get; set; }
}

public class TranslateTextRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public List<string> Contents { get; set; } = new();

    [Required]
    public string TargetLanguageCode { get; set; } = string.Empty;

    public string? SourceLanguageCode { get; set; }
    public string MimeType { get; set; } = "text/plain";
    public string? Model { get; set; }
    public GlossaryConfig? GlossaryConfig { get; set; }
}

public class GlossaryTranslation
{
    public string? TranslatedText { get; set; }
    public string? Model { get; set; }
}

public class Translation
{
    public string? TranslatedText { get; set; }
    public string? DetectedLanguageCode { get; set; }
    public string? Model { get; set; }
    public GlossaryTranslation? GlossaryTranslation { get; set; }
}

public class TranslateTextResponse
{
    public List<Translation>? Translations { get; set; }
    public List<Translation>? GlossaryTranslations { get; set; }
}

public class DetectLanguageRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    public string MimeType { get; set; } = "text/plain";
    public string? Model { get; set; }
}

public class DetectedLanguage
{
    public string? LanguageCode { get; set; }
    public double Confidence { get; set; }
}

public class DetectLanguageResponse
{
    public List<DetectedLanguage>? Languages { get; set; }
}

public class GetSupportedLanguagesRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    public string DisplayLanguageCode { get; set; } = "en";
    public string? Model { get; set; }
}

public class SupportedLanguage
{
    public string? LanguageCode { get; set; }
    public string? DisplayName { get; set; }
    public bool SupportSource { get; set; }
    public bool SupportTarget { get; set; }
}

public class GetSupportedLanguagesResponse
{
    public List<SupportedLanguage>? Languages { get; set; }
}

public class RomanizeTextRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public List<string> Contents { get; set; } = new();

    public string? SourceLanguageCode { get; set; }
}

public class Romanization
{
    public string? RomanizedText { get; set; }
    public string? DetectedLanguageCode { get; set; }
}

public class RomanizeTextResponse
{
    public List<Romanization>? Romanizations { get; set; }
}

public class BatchInputGcsSource
{
    public string Uri { get; set; } = string.Empty;
}

public class BatchInputConfig
{
    public BatchInputGcsSource? GcsSource { get; set; }
    public string MimeType { get; set; } = string.Empty;
}

public class BatchOutputGcsDestination
{
    public string OutputUriPrefix { get; set; } = string.Empty;
}

public class BatchOutputConfig
{
    public BatchOutputGcsDestination? GcsDestination { get; set; }
}

public class BatchTranslateTextRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    public string Location { get; set; } = "us-central1";
    public string? SourceLanguageCode { get; set; }

    [Required]
    public List<string> TargetLanguageCodes { get; set; } = new();

    [Required]
    public List<BatchInputConfig> InputConfigs { get; set; } = new();

    public BatchOutputConfig? OutputConfig { get; set; }
    public Dictionary<string, string>? Glossaries { get; set; }
    public Dictionary<string, string>? Labels { get; set; }
}

public class GlossaryInputConfig
{
    public BatchInputGcsSource? GcsSource { get; set; }
}

public class LanguagePair
{
    public string? SourceLanguageCode { get; set; }
    public string? TargetLanguageCode { get; set; }
}

public class CreateGlossaryRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    public string Location { get; set; } = "us-central1";

    [Required]
    public string GlossaryId { get; set; } = string.Empty;

    public LanguagePair? LanguagePair { get; set; }
    public GlossaryInputConfig? InputConfig { get; set; }
}

public class GlossaryResponse
{
    public string? Name { get; set; }
    public LanguagePair? LanguagePair { get; set; }
    public GlossaryInputConfig? InputConfig { get; set; }
    public int EntryCount { get; set; }
    public string? SubmitTime { get; set; }
    public string? EndTime { get; set; }
    public string? DisplayName { get; set; }
}

public class ListGlossariesResponse
{
    public List<GlossaryResponse>? Glossaries { get; set; }
    public string? NextPageToken { get; set; }
}

public class DeleteGlossaryRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string GlossaryName { get; set; } = string.Empty;

    public string Location { get; set; } = "us-central1";
}
