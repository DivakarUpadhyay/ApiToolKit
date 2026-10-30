using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.PromotionsPromotion;

/// <summary>Lists the promotions in your Merchant Center account.</summary>
public class ListPromotionsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
}

/// <summary>Retrieves the promotion from your Merchant Center account.</summary>
public class GetPromotionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>PromotionId (Required.)</summary>
    [Required] public string PromotionId { get; set; } = string.Empty;
}

/// <summary>Inserts a promotion for your Merchant Center account.</summary>
public class InsertPromotionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>InsertPromotionRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/promotions_v1</summary>
    public object? Body { get; set; }
}
