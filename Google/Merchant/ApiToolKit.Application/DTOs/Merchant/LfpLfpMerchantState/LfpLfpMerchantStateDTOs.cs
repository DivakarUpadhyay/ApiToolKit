using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.LfpLfpMerchantState;

/// <summary>Gets the LFP state of a merchant</summary>
public class GetLfpMerchantStateRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>LfpMerchantStateId (Required.)</summary>
    [Required] public string LfpMerchantStateId { get; set; } = string.Empty;
}
