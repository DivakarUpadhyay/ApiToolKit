using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.IssueresolutionAggregateProductStatuse;

/// <summary>Lists the `AggregateProductStatuses` resources for your merchant account.</summary>
public class ListAggregateProductStatusesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
    /// <summary>Optional.</summary>
    public string? Filter { get; set; }
}
