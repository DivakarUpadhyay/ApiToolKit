using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.FloodlightGroup;

/// <summary>Updates an existing Floodlight group.</summary>
public class UpdateFloodlightGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>FloodlightGroupId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string FloodlightGroupId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? PartnerId { get; set; }
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>FloodlightGroup JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Gets a Floodlight group.</summary>
public class GetFloodlightGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>FloodlightGroupId (Required.)</summary>
    [Required] public string FloodlightGroupId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? PartnerId { get; set; }
}
