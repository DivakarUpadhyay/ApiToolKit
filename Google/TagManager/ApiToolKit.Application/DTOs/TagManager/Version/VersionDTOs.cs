using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.TagManager.Version;

/// <summary>Get the live (published) container version.</summary>
public class GetLiveVersionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
}

/// <summary>List all container version headers.</summary>
public class ListVersionHeadersRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Include deleted versions</summary>
    public bool? IncludeDeleted { get; set; }
}

/// <summary>Get the latest container version header.</summary>
public class GetLatestVersionHeaderRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
}

/// <summary>Get one container version.</summary>
public class GetVersionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Container version id</summary>
    [Required] public string VersionId { get; set; } = string.Empty;
}

/// <summary>Publish a container version (makes it live).</summary>
public class PublishVersionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Container version id</summary>
    [Required] public string VersionId { get; set; } = string.Empty;
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}

/// <summary>Delete a container version.</summary>
public class DeleteVersionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Container version id</summary>
    [Required] public string VersionId { get; set; } = string.Empty;
}

/// <summary>Update a container version name or description.</summary>
public class UpdateVersionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Container version id</summary>
    [Required] public string VersionId { get; set; } = string.Empty;
    /// <summary>Version name</summary>
    public string? Name { get; set; }
    /// <summary>Description</summary>
    public string? Description { get; set; }
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}

/// <summary>Set a version as the latest version used for synchronization.</summary>
public class SetLatestVersionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Container version id</summary>
    [Required] public string VersionId { get; set; } = string.Empty;
}

/// <summary>Restore a deleted container version.</summary>
public class UndeleteVersionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Container version id</summary>
    [Required] public string VersionId { get; set; } = string.Empty;
}
