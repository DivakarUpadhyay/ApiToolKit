using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Drive;

public class DriveUser
{
    public string? DisplayName { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhotoLink { get; set; }
    public bool Me { get; set; }
}

public class FileCapabilities
{
    public bool CanEdit { get; set; }
    public bool CanShare { get; set; }
    public bool CanDownload { get; set; }
    public bool CanDelete { get; set; }
    public bool CanTrash { get; set; }
    public bool CanRename { get; set; }
    public bool CanMoveItemWithinDrive { get; set; }
}

public class FileResponse
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? MimeType { get; set; }
    public string? Description { get; set; }
    public long Size { get; set; }
    public DateTime? ModifiedTime { get; set; }
    public DateTime? CreatedTime { get; set; }
    public string? WebViewLink { get; set; }
    public string? WebContentLink { get; set; }
    public string? ThumbnailLink { get; set; }
    public List<string>? Parents { get; set; }
    public List<DriveUser>? Owners { get; set; }
    public DriveUser? LastModifyingUser { get; set; }
    public bool Shared { get; set; }
    public bool Starred { get; set; }
    public bool Trashed { get; set; }
    public long Version { get; set; }
    public FileCapabilities? Capabilities { get; set; }
    public string? FileExtension { get; set; }
    public string? FullFileExtension { get; set; }
    public string? Md5Checksum { get; set; }
}

public class ListFilesRequest
{
    public string? Q { get; set; }
    public int PageSize { get; set; } = 100;
    public string? PageToken { get; set; }
    public string OrderBy { get; set; } = "modifiedTime desc";
    public string? Fields { get; set; }
    public bool IncludeItemsFromAllDrives { get; set; } = false;
    public string? DriveId { get; set; }
}

public class FileListResponse
{
    public List<FileResponse>? Files { get; set; }
    public string? NextPageToken { get; set; }
    public bool IncompleteSearch { get; set; }
}

public class CreateFolderRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public List<string>? Parents { get; set; }
}

public class CreateFileMetadataRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? MimeType { get; set; }
    public List<string>? Parents { get; set; }
    public string? Description { get; set; }
}

public class UpdateFileRequest
{
    [Required]
    public string FileId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<string>? AddParents { get; set; }
    public List<string>? RemoveParents { get; set; }
    public bool? Starred { get; set; }
}

public class CopyFileRequest
{
    [Required]
    public string FileId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public List<string>? Parents { get; set; }
}

public class MoveFileRequest
{
    [Required]
    public string FileId { get; set; } = string.Empty;

    [Required]
    public string NewParentId { get; set; } = string.Empty;
}

public class DeleteFileRequest
{
    [Required]
    public string FileId { get; set; } = string.Empty;
}

public class TrashFileRequest
{
    [Required]
    public string FileId { get; set; } = string.Empty;
}

public class GetFileRequest
{
    [Required]
    public string FileId { get; set; } = string.Empty;
    public string? Fields { get; set; }
}

public class ShareFileRequest
{
    [Required]
    public string FileId { get; set; } = string.Empty;

    [Required]
    public string Type { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;
    public string? EmailAddress { get; set; }
    public string? Domain { get; set; }
    public bool SendNotificationEmail { get; set; } = true;
}
