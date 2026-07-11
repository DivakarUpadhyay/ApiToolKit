namespace ApiToolKit.Domain.Entities.Cloud;

public class LanguageAnalysis
{
    public long Id { get; set; }
    public string RequestId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string? Text { get; set; }
    public string AnalysisType { get; set; } = string.Empty;
    public string? ResultData { get; set; }
    public string? LanguageCode { get; set; }
    public double Confidence { get; set; }
    public DateTime CreatedAt { get; set; }
}
