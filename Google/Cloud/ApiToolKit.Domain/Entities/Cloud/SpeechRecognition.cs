namespace ApiToolKit.Domain.Entities.Cloud;

public class SpeechRecognition
{
    public long Id { get; set; }
    public string RequestId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string? AudioSource { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string? Transcript { get; set; }
    public double Confidence { get; set; }
    public int WordCount { get; set; }
    public double Duration { get; set; }
    public DateTime CreatedAt { get; set; }
}
