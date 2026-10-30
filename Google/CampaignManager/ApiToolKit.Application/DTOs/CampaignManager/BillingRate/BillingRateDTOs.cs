using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.BillingRate;

/// <summary>Retrieves a list of billing rates.</summary>
public class ListBillingRatesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Billing profile ID of this billing rate.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>BillingProfileId (Billing profile ID of this billing rate.)</summary>
    [Required] public string BillingProfileId { get; set; } = string.Empty;
}
