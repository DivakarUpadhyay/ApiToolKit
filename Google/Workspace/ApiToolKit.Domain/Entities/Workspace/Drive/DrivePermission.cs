namespace ApiToolKit.Domain.Entities.Workspace.Drive;

public class DrivePermission
{
    public int Id { get; set; }
    public string PermissionId { get; set; } = string.Empty;
    public string FileId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? Type { get; set; }
    public string? Role { get; set; }
    public string? EmailAddress { get; set; }
    public string? DisplayName { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public DateTime CreatedAt { get; set; }
}
