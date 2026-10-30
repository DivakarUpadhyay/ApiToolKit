using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.TagManager.Tag;

/// <summary>List tags in a workspace.</summary>
public class ListTagsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
}

/// <summary>Get one tag.</summary>
public class GetTagRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Tag id</summary>
    [Required] public string TagId { get; set; } = string.Empty;
}

/// <summary>Create a tag in a workspace.</summary>
public class CreateTagRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Tag name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>Tag type, e.g. gaawc (GA4 config), gaawe (GA4 event), html</summary>
    [Required] public string Type { get; set; } = string.Empty;
    /// <summary>Parameter array: [{ "type": "template", "key": "...", "value": "..." }]</summary>
    public object? Parameter { get; set; }
    /// <summary>Trigger ids that fire the tag</summary>
    public List<string>? FiringTriggerId { get; set; }
    /// <summary>Trigger ids that block the tag</summary>
    public List<string>? BlockingTriggerId { get; set; }
    /// <summary>Notes</summary>
    public string? Notes { get; set; }
    /// <summary>Pause the tag</summary>
    public bool? Paused { get; set; }
}

/// <summary>Update a tag (full replace).</summary>
public class UpdateTagRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Tag id</summary>
    [Required] public string TagId { get; set; } = string.Empty;
    /// <summary>Tag name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>Tag type, e.g. gaawc (GA4 config), gaawe (GA4 event), html</summary>
    [Required] public string Type { get; set; } = string.Empty;
    /// <summary>Parameter array: [{ "type": "template", "key": "...", "value": "..." }]</summary>
    public object? Parameter { get; set; }
    /// <summary>Trigger ids that fire the tag</summary>
    public List<string>? FiringTriggerId { get; set; }
    /// <summary>Trigger ids that block the tag</summary>
    public List<string>? BlockingTriggerId { get; set; }
    /// <summary>Notes</summary>
    public string? Notes { get; set; }
    /// <summary>Pause the tag</summary>
    public bool? Paused { get; set; }
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}

/// <summary>Delete a tag.</summary>
public class DeleteTagRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Tag id</summary>
    [Required] public string TagId { get; set; } = string.Empty;
}

/// <summary>Revert workspace changes to a tag.</summary>
public class RevertTagRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Tag id</summary>
    [Required] public string TagId { get; set; } = string.Empty;
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}
