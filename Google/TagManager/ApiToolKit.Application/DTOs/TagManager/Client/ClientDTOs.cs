using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.TagManager.Client;

/// <summary>List clients in a workspace.</summary>
public class ListClientsRequest
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

/// <summary>Get one client.</summary>
public class GetClientRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Client id</summary>
    [Required] public string ClientId { get; set; } = string.Empty;
}

/// <summary>Create a client.</summary>
public class CreateClientRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>Type</summary>
    [Required] public string Type { get; set; } = string.Empty;
    /// <summary>Parameter array: [{ "type": "template", "key": "...", "value": "..." }]</summary>
    public object? Parameter { get; set; }
    /// <summary>Notes</summary>
    public string? Notes { get; set; }
    /// <summary>Priority (server containers)</summary>
    public int? Priority { get; set; }
}

/// <summary>Update a client.</summary>
public class UpdateClientRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Client id</summary>
    [Required] public string ClientId { get; set; } = string.Empty;
    /// <summary>Name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>Type</summary>
    [Required] public string Type { get; set; } = string.Empty;
    /// <summary>Parameter array: [{ "type": "template", "key": "...", "value": "..." }]</summary>
    public object? Parameter { get; set; }
    /// <summary>Notes</summary>
    public string? Notes { get; set; }
    /// <summary>Priority (server containers)</summary>
    public int? Priority { get; set; }
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}

/// <summary>Delete a client.</summary>
public class DeleteClientRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Client id</summary>
    [Required] public string ClientId { get; set; } = string.Empty;
}

/// <summary>Revert workspace changes to a client.</summary>
public class RevertClientRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Client id</summary>
    [Required] public string ClientId { get; set; } = string.Empty;
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}
