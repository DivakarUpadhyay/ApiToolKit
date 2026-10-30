using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.DynamicProfile;

/// <summary>Generates code for a dynamic profile, which will need unescaping.</summary>
public class GenerateCodeDynamicProfileRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>DynamicProfileId (Required.)</summary>
    [Required] public string DynamicProfileId { get; set; } = string.Empty;
}

/// <summary>Updates an existing dynamic profile.</summary>
public class UpdateDynamicProfileRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>DynamicProfile JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Inserts a new dynamic profile.</summary>
public class InsertDynamicProfileRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>DynamicProfile JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Publish for a dynamic profile.</summary>
public class PublishDynamicProfileRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>DynamicProfileId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string DynamicProfileId { get; set; } = string.Empty;
}

/// <summary>Gets a dynamic profile by ID.</summary>
public class GetDynamicProfileRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>DynamicProfileId (Required.)</summary>
    [Required] public string DynamicProfileId { get; set; } = string.Empty;
}
