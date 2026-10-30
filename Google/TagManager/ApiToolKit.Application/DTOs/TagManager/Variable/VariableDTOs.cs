using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.TagManager.Variable;

/// <summary>List variables in a workspace.</summary>
public class ListVariablesRequest
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

/// <summary>Get one variable.</summary>
public class GetVariableRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Variable id</summary>
    [Required] public string VariableId { get; set; } = string.Empty;
}

/// <summary>Create a variable in a workspace.</summary>
public class CreateVariableRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Variable name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>Variable type, e.g. v (data layer), c (constant), jsm</summary>
    [Required] public string Type { get; set; } = string.Empty;
    /// <summary>Parameter array: [{ "type": "template", "key": "...", "value": "..." }]</summary>
    public object? Parameter { get; set; }
    /// <summary>Notes</summary>
    public string? Notes { get; set; }
}

/// <summary>Update a variable (full replace).</summary>
public class UpdateVariableRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Variable id</summary>
    [Required] public string VariableId { get; set; } = string.Empty;
    /// <summary>Variable name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>Variable type, e.g. v (data layer), c (constant), jsm</summary>
    [Required] public string Type { get; set; } = string.Empty;
    /// <summary>Parameter array: [{ "type": "template", "key": "...", "value": "..." }]</summary>
    public object? Parameter { get; set; }
    /// <summary>Notes</summary>
    public string? Notes { get; set; }
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}

/// <summary>Delete a variable.</summary>
public class DeleteVariableRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Variable id</summary>
    [Required] public string VariableId { get; set; } = string.Empty;
}

/// <summary>Revert workspace changes to a variable.</summary>
public class RevertVariableRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Variable id</summary>
    [Required] public string VariableId { get; set; } = string.Empty;
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}
