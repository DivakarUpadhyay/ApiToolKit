using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.CreativeGroup;

/// <summary>Gets one creative group by ID.</summary>
public class GetCreativeGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Creative group ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeGroupId (Creative group ID.)</summary>
    [Required] public string CreativeGroupId { get; set; } = string.Empty;
}

/// <summary>Updates an existing creative group.</summary>
public class UpdateCreativeGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeGroup JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing creative group.</summary>
public class PatchCreativeGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>CreativeGroup JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves a list of creative groups, possibly filtered.</summary>
public class ListCreativeGroupsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Allows searching for creative groups by name or ID.</summary>
    public string? SearchString { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Select only creative groups that belong to this subgroup.</summary>
    public int? GroupNumber { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Select only creative groups with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Select only creative groups that belong to these advertisers.</summary>
    public List<string>? AdvertiserIds { get; set; }
}

/// <summary>Inserts a new creative group.</summary>
public class InsertCreativeGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>CreativeGroup JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}
