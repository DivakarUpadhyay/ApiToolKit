using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.Creative;

/// <summary>Retrieves a list of creatives, possibly filtered.</summary>
public class ListCreativesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Select only in-stream video creatives with these companion IDs.</summary>
    public List<string>? CompanionCreativeIds { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Select only archived creatives.</summary>
    public bool? Archived { get; set; }
    /// <summary>Select only creatives corresponding to this Studio creative ID.</summary>
    public string? StudioCreativeId { get; set; }
    /// <summary>Select only creatives with this campaign ID.</summary>
    public string? CampaignId { get; set; }
    /// <summary>Select only creatives with these size IDs.</summary>
    public List<string>? SizeIds { get; set; }
    /// <summary>Select only creatives with this advertiser ID.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Select only creatives with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Select only active creatives.</summary>
    public bool? Active { get; set; }
    /// <summary>Allows searching for objects by name or ID.</summary>
    public string? SearchString { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Select only creatives with these creative field IDs.</summary>
    public List<string>? CreativeFieldIds { get; set; }
    /// <summary>Select only creatives with these creative types.</summary>
    public List<string>? Types { get; set; }
    /// <summary>Select only creatives with these rendering IDs.</summary>
    public List<string>? RenderingIds { get; set; }
}

/// <summary>Updates an existing creative.</summary>
public class UpdateCreativeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>Creative JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing creative.</summary>
public class UpdateCreativeAltRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Creative JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Gets one creative by ID.</summary>
public class GetCreativeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeId (User profile ID associated with this request.)</summary>
    [Required] public string CreativeId { get; set; } = string.Empty;
}

/// <summary>Inserts a new creative.</summary>
public class InsertCreativeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Creative JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}
