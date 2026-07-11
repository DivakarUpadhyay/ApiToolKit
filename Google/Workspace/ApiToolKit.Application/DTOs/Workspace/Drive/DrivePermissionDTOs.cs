using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Drive;

public class PermissionResponse
{
    public string? Id { get; set; }
    public string? Type { get; set; }
    public string? Role { get; set; }
    public string? EmailAddress { get; set; }
    public string? DisplayName { get; set; }
    public string? PhotoLink { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public bool Deleted { get; set; }
    public bool PendingOwner { get; set; }
}

public class ListPermissionsResponse
{
    public List<PermissionResponse>? Permissions { get; set; }
    public string? NextPageToken { get; set; }
}

public class CreatePermissionRequest
{
    [Required]
    public string FileId { get; set; } = string.Empty;

    [Required]
    public string Type { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;
    public string? EmailAddress { get; set; }
    public string? Domain { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public bool SendNotificationEmail { get; set; } = true;
    public bool TransferOwnership { get; set; } = false;
}

public class UpdatePermissionRequest
{
    [Required]
    public string FileId { get; set; } = string.Empty;

    [Required]
    public string PermissionId { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;
    public DateTime? ExpirationTime { get; set; }
    public bool RemoveExpiration { get; set; } = false;
}

public class DeletePermissionRequest
{
    [Required]
    public string FileId { get; set; } = string.Empty;

    [Required]
    public string PermissionId { get; set; } = string.Empty;
}
