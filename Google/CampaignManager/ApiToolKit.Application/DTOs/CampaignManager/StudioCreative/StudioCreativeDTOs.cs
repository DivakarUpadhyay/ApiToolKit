using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.StudioCreative;

/// <summary>Gets a studio creative by ID.</summary>
public class GetStudioCreativeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>CreativeId (Required.)</summary>
    [Required] public string CreativeId { get; set; } = string.Empty;
}

/// <summary>Inserts a new studio creative.</summary>
public class InsertStudioCreativeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>StudioCreative JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Publish for a studio creative.</summary>
public class PublishStudioCreativeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>CreativeId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string CreativeId { get; set; } = string.Empty;
}
