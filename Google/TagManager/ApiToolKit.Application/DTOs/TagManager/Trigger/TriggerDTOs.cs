using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.TagManager.Trigger;

/// <summary>List triggers in a workspace.</summary>
public class ListTriggersRequest
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

/// <summary>Get one trigger.</summary>
public class GetTriggerRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Trigger id</summary>
    [Required] public string TriggerId { get; set; } = string.Empty;
}

/// <summary>Create a trigger in a workspace.</summary>
public class CreateTriggerRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Trigger name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>pageview, click, customEvent, formSubmission, ...</summary>
    [Required] public string Type { get; set; } = string.Empty;
    /// <summary>Condition array</summary>
    public object? Filter { get; set; }
    /// <summary>Condition array for customEvent</summary>
    public object? CustomEventFilter { get; set; }
    /// <summary>Condition array for auto events</summary>
    public object? AutoEventFilter { get; set; }
    /// <summary>Notes</summary>
    public string? Notes { get; set; }
}

/// <summary>Update a trigger (full replace).</summary>
public class UpdateTriggerRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Trigger id</summary>
    [Required] public string TriggerId { get; set; } = string.Empty;
    /// <summary>Trigger name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>pageview, click, customEvent, formSubmission, ...</summary>
    [Required] public string Type { get; set; } = string.Empty;
    /// <summary>Condition array</summary>
    public object? Filter { get; set; }
    /// <summary>Condition array for customEvent</summary>
    public object? CustomEventFilter { get; set; }
    /// <summary>Condition array for auto events</summary>
    public object? AutoEventFilter { get; set; }
    /// <summary>Notes</summary>
    public string? Notes { get; set; }
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}

/// <summary>Delete a trigger.</summary>
public class DeleteTriggerRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Trigger id</summary>
    [Required] public string TriggerId { get; set; } = string.Empty;
}

/// <summary>Revert workspace changes to a trigger.</summary>
public class RevertTriggerRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Workspace id</summary>
    [Required] public string WorkspaceId { get; set; } = string.Empty;
    /// <summary>Trigger id</summary>
    [Required] public string TriggerId { get; set; } = string.Empty;
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}
