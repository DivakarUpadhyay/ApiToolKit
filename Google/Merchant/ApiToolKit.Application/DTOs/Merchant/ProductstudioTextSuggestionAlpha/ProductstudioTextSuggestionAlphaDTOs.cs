using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.ProductstudioTextSuggestionAlpha;

/// <summary>GenerateProductTextSuggestions generates a set of candidate text completions (e.g., product titles, descriptions) based on provided product information.</summary>
public class GenerateProductTextSuggestionsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>GenerateProductTextSuggestionsRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/productstudio_v1alpha</summary>
    public object? Body { get; set; }
}
