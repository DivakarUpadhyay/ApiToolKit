using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Drive;

public class RevisionResponse
{
    public string? Id { get; set; }
    public string? MimeType { get; set; }
    public DateTime? ModifiedTime { get; set; }
    public bool KeepForever { get; set; }
    public bool Published { get; set; }
    public bool PublishAuto { get; set; }
    public DriveUser? LastModifyingUser { get; set; }
    public long Size { get; set; }
    public string? OriginalFilename { get; set; }
}

public class ListRevisionsResponse
{
    public List<RevisionResponse>? Revisions { get; set; }
    public string? NextPageToken { get; set; }
}

public class UpdateRevisionRequest
{
    [Required]
    public string FileId { get; set; } = string.Empty;

    [Required]
    public string RevisionId { get; set; } = string.Empty;
    public bool KeepForever { get; set; }
    public bool Published { get; set; }
    public bool PublishAuto { get; set; }
}

public class DeleteRevisionRequest
{
    [Required]
    public string FileId { get; set; } = string.Empty;

    [Required]
    public string RevisionId { get; set; } = string.Empty;
}
