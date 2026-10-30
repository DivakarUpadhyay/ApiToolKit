using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.OrdertrackingOrderTrackingSignal;

/// <summary>Creates new order tracking signal.</summary>
public class CreateOrderTrackingSignalRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>Output only.</summary>
    public string? OrderTrackingSignalId { get; set; }
    /// <summary>OrderTrackingSignal JSON object as documented at https://developers.google.com/merchant/api/reference/rest/ordertracking_v1</summary>
    public object? Body { get; set; }
}
