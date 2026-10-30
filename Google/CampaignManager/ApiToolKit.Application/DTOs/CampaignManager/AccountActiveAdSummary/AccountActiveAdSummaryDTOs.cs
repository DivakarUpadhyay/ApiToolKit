using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.AccountActiveAdSummary;

/// <summary>Gets the account's active ad summary by account ID.</summary>
public class GetAccountActiveAdSummaryRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Account ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>AccountActiveAdSummaryId (Account ID.)</summary>
    [Required] public string AccountActiveAdSummaryId { get; set; } = string.Empty;
}
