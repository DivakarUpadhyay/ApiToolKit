namespace ApiToolKit.Domain.Entities.Workspace.Forms;

public class FormResponse
{
    public int Id { get; set; }
    public string ResponseId { get; set; } = string.Empty;
    public string FormId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? CreateTime { get; set; }
    public string? LastSubmittedTime { get; set; }
    public string? RespondentEmail { get; set; }
    public double? TotalScore { get; set; }
    public string? Answers { get; set; }
    public DateTime CreatedAt { get; set; }
}
