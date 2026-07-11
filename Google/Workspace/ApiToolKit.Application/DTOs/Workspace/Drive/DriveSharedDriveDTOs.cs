using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Drive;

public class SharedDriveCapabilities
{
    public bool CanAddChildren { get; set; }
    public bool CanDeleteDrive { get; set; }
    public bool CanDownload { get; set; }
    public bool CanEdit { get; set; }
    public bool CanListChildren { get; set; }
    public bool CanManageMembers { get; set; }
    public bool CanReadRevisions { get; set; }
    public bool CanShare { get; set; }
}

public class SharedDriveRestrictions
{
    public bool AdminManagedRestrictions { get; set; }
    public bool CopyRequiresWriterPermission { get; set; }
    public bool DomainUsersOnly { get; set; }
    public bool DriveMembersOnly { get; set; }
}

public class SharedDriveResponse
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Kind { get; set; }
    public DateTime? CreatedTime { get; set; }
    public SharedDriveCapabilities? Capabilities { get; set; }
    public SharedDriveRestrictions? Restrictions { get; set; }
    public string? BackgroundImageLink { get; set; }
    public string? ColorRgb { get; set; }
    public string? ThemeId { get; set; }
}

public class ListSharedDrivesResponse
{
    public List<SharedDriveResponse>? Drives { get; set; }
    public string? NextPageToken { get; set; }
}

public class CreateSharedDriveRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? RequestId { get; set; }
}

public class UpdateSharedDriveRequest
{
    [Required]
    public string DriveId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? ColorRgb { get; set; }
}
