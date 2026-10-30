using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.OmnichannelSettingLfpProvider;

/// <summary>Find the LFP provider candidates in a given country.</summary>
public class FindLfpProviderRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>OmnichannelSettingId (Required.)</summary>
    [Required] public string OmnichannelSettingId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
}

/// <summary>Link the specified merchant to a LFP provider for the specified country.</summary>
public class LinkLfpProviderRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>OmnichannelSettingId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string OmnichannelSettingId { get; set; } = string.Empty;
    /// <summary>LfpProviderId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string LfpProviderId { get; set; } = string.Empty;
    /// <summary>LinkLfpProviderRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}
