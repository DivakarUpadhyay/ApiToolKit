using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.TagManager.Destination;

/// <summary>List destinations linked to a container.</summary>
public class ListDestinationsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
}

/// <summary>Get one linked destination.</summary>
public class GetDestinationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Destination link id</summary>
    [Required] public string DestinationLinkId { get; set; } = string.Empty;
}

/// <summary>Link a destination (e.g. G-XXXX) to a container.</summary>
public class LinkDestinationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Tag Manager account id</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Container id</summary>
    [Required] public string ContainerId { get; set; } = string.Empty;
    /// <summary>Destination id, e.g. G-XXXXXXX or AW-XXXXXXX</summary>
    [Required] public string DestinationId { get; set; } = string.Empty;
    /// <summary>Allow permission updates</summary>
    public bool? AllowUserPermissionFeatureUpdate { get; set; }
}
