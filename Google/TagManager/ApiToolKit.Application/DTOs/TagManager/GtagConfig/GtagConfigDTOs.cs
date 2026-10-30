using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.TagManager.GtagConfig;

/// <summary>List gtagconfigs in a workspace.</summary>
public class ListGtagConfigsRequest
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

/// <summary>Get one gtagconfig.</summary>
public class GetGtagConfigRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>GtagConfig id</summary>
    [Required] public string GtagConfigId { get; set; } = string.Empty;
}

/// <summary>Create a gtagconfig.</summary>
public class CreateGtagConfigRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Google tag config type</summary>
    [Required] public string Type { get; set; } = string.Empty;
    /// <summary>Parameter array: [{ "type": "template", "key": "...", "value": "..." }]</summary>
    public object? Parameter { get; set; }
}

/// <summary>Update a gtagconfig.</summary>
public class UpdateGtagConfigRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>GtagConfig id</summary>
    [Required] public string GtagConfigId { get; set; } = string.Empty;
    /// <summary>Google tag config type</summary>
    [Required] public string Type { get; set; } = string.Empty;
    /// <summary>Parameter array: [{ "type": "template", "key": "...", "value": "..." }]</summary>
    public object? Parameter { get; set; }
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}

/// <summary>Delete a gtagconfig.</summary>
public class DeleteGtagConfigRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>GtagConfig id</summary>
    [Required] public string GtagConfigId { get; set; } = string.Empty;
}
