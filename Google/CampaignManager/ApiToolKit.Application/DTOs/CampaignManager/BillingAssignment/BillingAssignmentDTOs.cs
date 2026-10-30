using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.BillingAssignment;

/// <summary>Inserts a new billing assignment and returns the new assignment.</summary>
public class InsertBillingAssignmentRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Billing profile ID of this billing assignment.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>BillingProfileId (Billing profile ID of this billing assignment.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string BillingProfileId { get; set; } = string.Empty;
    /// <summary>BillingAssignment JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves a list of billing assignments.</summary>
public class ListBillingAssignmentsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>BillingProfileId (User profile ID associated with this request.)</summary>
    [Required] public string BillingProfileId { get; set; } = string.Empty;
}
