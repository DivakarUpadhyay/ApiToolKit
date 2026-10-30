using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.QuotaLimit;

/// <summary>Lists the limits of an account.</summary>
public class ListLimitsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
    /// <summary>Required.</summary>
    public string? Filter { get; set; }
}

/// <summary>Retrieves an account limit.</summary>
public class GetLimitRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>LimitId (Required.)</summary>
    [Required] public string LimitId { get; set; } = string.Empty;
}
