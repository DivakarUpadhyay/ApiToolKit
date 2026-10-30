using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.QuotaQuota;

/// <summary>Lists the daily call quota and usage per group for your Merchant Center account.</summary>
public class ListQuotasRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
}
