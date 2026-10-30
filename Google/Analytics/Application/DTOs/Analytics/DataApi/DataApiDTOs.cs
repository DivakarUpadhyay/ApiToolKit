using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace ApiToolKit.Application.DTOs.Analytics.DataApi;

/// <summary>
/// A GA4 property id plus the raw JSON request body Google documents for the method.
/// </summary>
public class PropertyBodyRequest
{
    /// <summary>GA4 property id (number only)</summary>
    [Required] public string PropertyId { get; set; } = string.Empty;

    /// <summary>Request body exactly as documented by the Google Analytics Data API</summary>
    public JsonObject? Body { get; set; }
}

/// <summary>
/// A GA4 property id, a child resource id (audience export, audience list, report task) and an optional JSON body.
/// </summary>
public class PropertyResourceBodyRequest : PropertyBodyRequest
{
    /// <summary>Child resource id</summary>
    [Required] public string ResourceId { get; set; } = string.Empty;
}
