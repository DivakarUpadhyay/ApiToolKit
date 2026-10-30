using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.TagManager.Zone;

/// <summary>List zones in a workspace.</summary>
public class ListZonesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Next page token</summary>
    public string? PageToken { get; set; }
}

/// <summary>Get one zone.</summary>
public class GetZoneRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Zone id</summary>
    [Required] public string ZoneId { get; set; } = string.Empty;
}

/// <summary>Create a zone.</summary>
public class CreateZoneRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Zone name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>Array of child containers</summary>
    public object? ChildContainer { get; set; }
    /// <summary>Zone boundary</summary>
    public object? Boundary { get; set; }
    /// <summary>Allowed tag types</summary>
    public object? TypeRestriction { get; set; }
    /// <summary>Notes</summary>
    public string? Notes { get; set; }
}

/// <summary>Update a zone.</summary>
public class UpdateZoneRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Zone id</summary>
    [Required] public string ZoneId { get; set; } = string.Empty;
    /// <summary>Zone name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>Array of child containers</summary>
    public object? ChildContainer { get; set; }
    /// <summary>Zone boundary</summary>
    public object? Boundary { get; set; }
    /// <summary>Allowed tag types</summary>
    public object? TypeRestriction { get; set; }
    /// <summary>Notes</summary>
    public string? Notes { get; set; }
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}

/// <summary>Delete a zone.</summary>
public class DeleteZoneRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Zone id</summary>
    [Required] public string ZoneId { get; set; } = string.Empty;
}

/// <summary>Revert workspace changes to a zone.</summary>
public class RevertZoneRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Zone id</summary>
    [Required] public string ZoneId { get; set; } = string.Empty;
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}
