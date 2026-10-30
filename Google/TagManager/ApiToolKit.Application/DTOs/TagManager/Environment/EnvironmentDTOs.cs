using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.TagManager.Environment;

/// <summary>List environments of a container.</summary>
public class ListEnvironmentsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
}

/// <summary>Get one environment.</summary>
public class GetEnvironmentRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Environment id</summary>
    [Required] public string EnvironmentId { get; set; } = string.Empty;
}

/// <summary>Create a user environment.</summary>
public class CreateEnvironmentRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Environment name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>Description</summary>
    public string? Description { get; set; }
    /// <summary>Default preview URL</summary>
    public string? Url { get; set; }
    /// <summary>Enable debug mode by default</summary>
    public bool? EnableDebug { get; set; }
    /// <summary>Version published to this environment</summary>
    public string? ContainerVersionId { get; set; }
}

/// <summary>Update an environment.</summary>
public class UpdateEnvironmentRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Environment id</summary>
    [Required] public string EnvironmentId { get; set; } = string.Empty;
    /// <summary>Environment name</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>Description</summary>
    public string? Description { get; set; }
    /// <summary>Default preview URL</summary>
    public string? Url { get; set; }
    /// <summary>Enable debug mode</summary>
    public bool? EnableDebug { get; set; }
    /// <summary>Version published to this environment</summary>
    public string? ContainerVersionId { get; set; }
    /// <summary>Fingerprint for optimistic locking</summary>
    public string? Fingerprint { get; set; }
}

/// <summary>Delete an environment.</summary>
public class DeleteEnvironmentRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Environment id</summary>
    [Required] public string EnvironmentId { get; set; } = string.Empty;
}

/// <summary>Regenerate the authorization code of an environment.</summary>
public class ReauthorizeEnvironmentRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Environment id</summary>
    [Required] public string EnvironmentId { get; set; } = string.Empty;
}
