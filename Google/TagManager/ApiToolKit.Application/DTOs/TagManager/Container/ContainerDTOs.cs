using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.TagManager.Container;

/// <summary>List containers in an account.</summary>
public class ListContainersRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
}

/// <summary>Get one container.</summary>
public class GetContainerRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
}

/// <summary>Create a container.</summary>
public class CreateContainerRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>web, android, ios, amp, server</summary>
    [Required] public List<string> UsageContext { get; set; } = new();
    /// <summary>Domains</summary>
    public List<string>? DomainName { get; set; }
    /// <summary>Notes</summary>
    public string? Notes { get; set; }
}

/// <summary>Update a container.</summary>
public class UpdateContainerRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Container name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>web, android, ios, amp, server</summary>
    [Required] public List<string> UsageContext { get; set; } = new();
    /// <summary>Domains</summary>
    public List<string>? DomainName { get; set; }
    /// <summary>Notes</summary>
    public string? Notes { get; set; }
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}

/// <summary>Delete a container.</summary>
public class DeleteContainerRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
}

/// <summary>Get the GTM install snippet for a container.</summary>
public class GetContainerSnippetRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
}

/// <summary>Find a container by a destination id (e.g. G-XXXX, AW-XXXX) or tag id.</summary>
public class LookupContainerRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Destination id</summary>
    public string? DestinationId { get; set; }
    /// <summary>Tag id</summary>
    public string? TagId { get; set; }
}

/// <summary>Combine another container into this one.</summary>
public class CombineContainersRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Container id to merge in</summary>
    [Required] public string SourceContainerId { get; set; } = string.Empty;
    /// <summary>CURRENT or OTHER</summary>
    public string? SettingSource { get; set; }
    /// <summary>Allow permission updates</summary>
    public bool? AllowUserPermissionFeatureUpdate { get; set; }
}

/// <summary>Move a Google tag id out of a container into a new destination container.</summary>
public class MoveTagIdRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Tag id to move</summary>
    [Required] public string TagId { get; set; } = string.Empty;
    /// <summary>Name of the new Google tag</summary>
    public string? TagName { get; set; }
    /// <summary>Copy settings</summary>
    public bool? CopySettings { get; set; }
    /// <summary>Copy terms of service</summary>
    public bool? CopyTermsOfService { get; set; }
    /// <summary>Copy users</summary>
    public bool? CopyUsers { get; set; }
    /// <summary>Allow permission updates</summary>
    public bool? AllowUserPermissionFeatureUpdate { get; set; }
}
