using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.FloodlightGroupFloodlightActivity;

/// <summary>Lists Floodlight activities in a Floodlight group.</summary>
public class ListFloodlightActivitiesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>FloodlightGroupId (Required.)</summary>
    [Required] public string FloodlightGroupId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
    /// <summary>Optional.</summary>
    public string? OrderBy { get; set; }
    /// <summary>Required.</summary>
    public string? PartnerId { get; set; }
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
}

/// <summary>Gets a Floodlight activity.</summary>
public class GetFloodlightActivityRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>FloodlightGroupId (Required.)</summary>
    [Required] public string FloodlightGroupId { get; set; } = string.Empty;
    /// <summary>FloodlightActivityId (Required.)</summary>
    [Required] public string FloodlightActivityId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? PartnerId { get; set; }
}
