using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.BusinessProfile.Lodging;

/// <summary>Get lodging attributes of a hotel location.</summary>
public class GetLodgingRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Fields, default *</summary>
    public string? ReadMask { get; set; }
}

/// <summary>Update lodging attributes (fields in UpdateMask).</summary>
public class UpdateLodgingRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Comma-separated fields to update</summary>
    [Required] public string UpdateMask { get; set; } = string.Empty;
    /// <summary>Lodging object with the fields to change</summary>
    [Required] public object Lodging { get; set; } = new();
}

/// <summary>Get Google-updated lodging attributes.</summary>
public class GetGoogleUpdatedLodgingRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Fields, default *</summary>
    public string? ReadMask { get; set; }
}
