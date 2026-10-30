using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.ReviewsMerchantReviewAlpha;

/// <summary>Deletes merchant review.</summary>
public class DeleteMerchantReviewRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>MerchantReviewId (Required.)</summary>
    [Required] public string MerchantReviewId { get; set; } = string.Empty;
}

/// <summary>Gets a merchant review.</summary>
public class GetMerchantReviewRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>MerchantReviewId (Required.)</summary>
    [Required] public string MerchantReviewId { get; set; } = string.Empty;
}

/// <summary>Lists merchant reviews.</summary>
public class ListMerchantReviewsRequest
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

/// <summary>Inserts a review for your Merchant Center account.</summary>
public class InsertMerchantReviewRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? DataSource { get; set; }
    /// <summary>MerchantReview JSON object as documented at https://developers.google.com/merchant/api/reference/rest/reviews_v1alpha</summary>
    public object? Body { get; set; }
}
