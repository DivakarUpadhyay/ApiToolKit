using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.BillingProfile;

/// <summary>Retrieves a list of billing profiles, possibly filtered.</summary>
public class ListBillingProfilesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Order of sorted results.</summary>
    public string? SortOrder { get; set; }
    /// <summary>Select only billing profile with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Select only billing profile which is suggested for the currency_code &amp; subaccount_id using the Billing Suggestion API.</summary>
    public bool? OnlySuggestion { get; set; }
    /// <summary>Select only billing profile with the specified subaccount.When only_suggestion is true, only a single subaccount_id is supported.</summary>
    public List<string>? SubaccountIds { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Select only billing profile with currency.</summary>
    public string? CurrencyCode { get; set; }
    /// <summary>Select only billing profile with the specified status.</summary>
    public List<string>? Status { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? SortField { get; set; }
    /// <summary>Allows searching for billing profiles by name.</summary>
    public string? Name { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
}

/// <summary>Gets one billing profile by ID.</summary>
public class GetBillingProfileRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>BillingProfileId (User profile ID associated with this request.)</summary>
    [Required] public string BillingProfileId { get; set; } = string.Empty;
}

/// <summary>Updates an existing billing profile.</summary>
public class UpdateBillingProfileRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>BillingProfile JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}
