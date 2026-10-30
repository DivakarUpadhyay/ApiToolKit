using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.PlacementGroup;

/// <summary>Updates an existing placement group.</summary>
public class UpdatePlacementGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>PlacementGroup JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Gets one placement group by ID.</summary>
public class GetPlacementGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>PlacementGroupId (User profile ID associated with this request.)</summary>
    [Required] public string PlacementGroupId { get; set; } = string.Empty;
}

/// <summary>Updates an existing placement group.</summary>
public class PatchPlacementGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>PlacementGroup JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Inserts a new placement group.</summary>
public class InsertPlacementGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>PlacementGroup JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves a list of placement groups, possibly filtered.</summary>
public class ListPlacementGroupsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Select only placement groups with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Select only placement groups that are associated with these content categories.</summary>
    public List<string>? ContentCategoryIds { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Select only placements with these active statuses.</summary>
    public List<string>? ActiveStatus { get; set; }
    /// <summary>Select only placement groups that are associated with these sites.</summary>
    public List<string>? SiteIds { get; set; }
    /// <summary>Select only placement groups with these pricing types.</summary>
    public List<string>? PricingTypes { get; set; }
    /// <summary>Select only placement groups that are associated with these placement strategies.</summary>
    public List<string>? PlacementStrategyIds { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Select only placement groups that are associated with these directory sites.</summary>
    public List<string>? DirectorySiteIds { get; set; }
    /// <summary>Select only placements or placement groups whose end date is on or after the specified minEndDate.</summary>
    public string? MinEndDate { get; set; }
    /// <summary>Select only placements or placement groups whose start date is on or after the specified minStartDate.</summary>
    public string? MinStartDate { get; set; }
    /// <summary>Select only placement groups belonging with this group type.</summary>
    public string? PlacementGroupType { get; set; }
    /// <summary>Select only placement groups that belong to these campaigns.</summary>
    public List<string>? CampaignIds { get; set; }
    /// <summary>Select only placement groups that belong to these advertisers.</summary>
    public List<string>? AdvertiserIds { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Allows searching for placement groups by name or ID.</summary>
    public string? SearchString { get; set; }
    /// <summary>Select only placements or placement groups whose start date is on or before the specified maxStartDate.</summary>
    public string? MaxStartDate { get; set; }
    /// <summary>Select only placements or placement groups whose end date is on or before the specified maxEndDate.</summary>
    public string? MaxEndDate { get; set; }
}
