namespace ApiToolKit.Domain.Entities.Firebase;

public class RemoteConfigTemplate
{
    public int Id { get; set; }
    public string ProjectId { get; set; } = string.Empty;
    public string? ETag { get; set; }
    public string? Version { get; set; }
    public string? UpdateTime { get; set; }
    public string? UpdateOrigin { get; set; }
    public string? UpdateType { get; set; }
    public string? UpdateUser { get; set; }
    public string? Description { get; set; }
    public string? Parameters { get; set; }
    public string? Conditions { get; set; }
    public DateTime CreatedAt { get; set; }
}
