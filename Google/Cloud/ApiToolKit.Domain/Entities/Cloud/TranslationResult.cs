namespace ApiToolKit.Domain.Entities.Cloud;

public class TranslationResult
{
    public long Id { get; set; }
    public string RequestId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string? SourceText { get; set; }
    public string? TranslatedText { get; set; }
    public string? SourceLanguage { get; set; }
    public string TargetLanguage { get; set; } = string.Empty;
    public string? GlossaryId { get; set; }
    public string? Model { get; set; }
    public DateTime CreatedAt { get; set; }
}
