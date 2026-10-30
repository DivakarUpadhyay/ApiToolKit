using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.Campaign;

/// <summary>Creates a new campaign.</summary>
public class CreateCampaignRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>Campaign JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Lists campaigns in an advertiser.</summary>
public class ListCampaignsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (The ID of the advertiser to list campaigns for.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>Allows filtering by campaign fields.</summary>
    public string? Filter { get; set; }
}

/// <summary>Updates an existing campaign.</summary>
public class UpdateCampaignRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>CampaignId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string CampaignId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>Campaign JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Gets a campaign.</summary>
public class GetCampaignRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>CampaignId (Required.)</summary>
    [Required] public string CampaignId { get; set; } = string.Empty;
}

/// <summary>Permanently deletes a campaign.</summary>
public class DeleteCampaignRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (The ID of the advertiser this campaign belongs to.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>CampaignId (The ID of the advertiser this campaign belongs to.)</summary>
    [Required] public string CampaignId { get; set; } = string.Empty;
}
