using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.ShippingSetting;

/// <summary>Replace the shipping setting of a business with the request shipping setting.</summary>
public class InsertShippingSettingRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>ShippingSettings JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieve shipping setting information.</summary>
public class GetShippingSettingsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
}
