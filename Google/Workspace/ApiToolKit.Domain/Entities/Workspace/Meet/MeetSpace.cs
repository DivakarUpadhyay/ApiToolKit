namespace ApiToolKit.Domain.Entities.Workspace.Meet;

public class MeetSpace
{
    public int Id { get; set; }
    public string SpaceName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? MeetingCode { get; set; }
    public string? MeetingUri { get; set; }
    public string? Title { get; set; }
    public string? ActiveConference { get; set; }
    public string? Config { get; set; }
    public DateTime CreatedAt { get; set; }
}
