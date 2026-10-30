using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.Placement;

/// <summary>Updates an existing placement.</summary>
public class UpdatePlacementRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>Placement JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Gets one placement by ID.</summary>
public class GetPlacementRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>PlacementId (User profile ID associated with this request.)</summary>
    [Required] public string PlacementId { get; set; } = string.Empty;
}

/// <summary>Inserts a new placement.</summary>
public class InsertPlacementRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Placement JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Generates tags for a placement.</summary>
public class GeneratetagsPlacementRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Tag formats to generate for these placements.</summary>
    public List<string>? TagFormats { get; set; }
    /// <summary>Optional.</summary>
    public bool? TagPropertiesDcDbmMacroIncluded { get; set; }
    /// <summary>Optional.</summary>
    public bool? TagPropertiesGppMacrosIncluded { get; set; }
    /// <summary>Generate tags for these placements.</summary>
    public List<string>? PlacementIds { get; set; }
    /// <summary>Generate placements belonging to this campaign.</summary>
    public string? CampaignId { get; set; }
    /// <summary>Optional.</summary>
    public bool? TagPropertiesTcfGdprMacrosIncluded { get; set; }
}

/// <summary>Updates an existing placement.</summary>
public class UpdatePlacementAltRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Placement JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves a list of placements, possibly filtered.</summary>
public class ListPlacementsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Select only placements that belong to these campaigns.</summary>
    public List<string>? CampaignIds { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Select only placements with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Allows searching for placements by name or ID.</summary>
    public string? SearchString { get; set; }
    /// <summary>Select only placements or placement groups whose start date is on or before the specified maxStartDate.</summary>
    public string? MaxStartDate { get; set; }
    /// <summary>Select only placements that belong to these advertisers.</summary>
    public List<string>? AdvertiserIds { get; set; }
    /// <summary>Select only placements that are associated with these content categories.</summary>
    public List<string>? ContentCategoryIds { get; set; }
    /// <summary>Select only placements or placement groups whose end date is on or after the specified minEndDate.</summary>
    public string? MinEndDate { get; set; }
    /// <summary>Select only placements that are associated with these compatibilities.</summary>
    public List<string>? Compatibilities { get; set; }
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Select only placements that are associated with these sizes.</summary>
    public List<string>? SizeIds { get; set; }
    /// <summary>Select only placements with these pricing types.</summary>
    public List<string>? PricingTypes { get; set; }
    /// <summary>Select only placements that are associated with these placement strategies.</summary>
    public List<string>? PlacementStrategyIds { get; set; }
    /// <summary>Select only placements with these active statuses.</summary>
    public List<string>? ActiveStatus { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Select only placements that are associated with these sites.</summary>
    public List<string>? SiteIds { get; set; }
    /// <summary>Select only placements or placement groups whose start date is on or after the specified minStartDate.</summary>
    public string? MinStartDate { get; set; }
    /// <summary>Select only placements or placement groups whose end date is on or before the specified maxEndDate.</summary>
    public string? MaxEndDate { get; set; }
    /// <summary>Select only placements that belong to these placement groups.</summary>
    public List<string>? GroupIds { get; set; }
    /// <summary>Select only placements with this payment source.</summary>
    public string? PaymentSource { get; set; }
    /// <summary>Select only placements that are associated with these directory sites.</summary>
    public List<string>? DirectorySiteIds { get; set; }
}
