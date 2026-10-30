using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.TagManager.Transformation;

/// <summary>List transformations in a workspace.</summary>
public class ListTransformationsRequest
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

/// <summary>Get one transformation.</summary>
public class GetTransformationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Transformation id</summary>
    [Required] public string TransformationId { get; set; } = string.Empty;
}

/// <summary>Create a transformation.</summary>
public class CreateTransformationRequest
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
}

/// <summary>Update a transformation.</summary>
public class UpdateTransformationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Transformation id</summary>
    [Required] public string TransformationId { get; set; } = string.Empty;
    /// <summary>Name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>Type</summary>
    [Required] public string Type { get; set; } = string.Empty;
    /// <summary>Parameter array: [{ "type": "template", "key": "...", "value": "..." }]</summary>
    public object? Parameter { get; set; }
    /// <summary>Notes</summary>
    public string? Notes { get; set; }
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}

/// <summary>Delete a transformation.</summary>
public class DeleteTransformationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Transformation id</summary>
    [Required] public string TransformationId { get; set; } = string.Empty;
}

/// <summary>Revert workspace changes to a transformation.</summary>
public class RevertTransformationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Transformation id</summary>
    [Required] public string TransformationId { get; set; } = string.Empty;
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}
