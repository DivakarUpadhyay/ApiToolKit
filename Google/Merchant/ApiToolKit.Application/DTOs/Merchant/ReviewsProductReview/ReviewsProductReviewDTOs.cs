using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.ReviewsProductReview;

/// <summary>Gets a product review.</summary>
public class GetProductReviewRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProductReviewId (Required.)</summary>
    [Required] public string ProductReviewId { get; set; } = string.Empty;
}

/// <summary>Lists product reviews.</summary>
public class ListProductReviewsRequest
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

/// <summary>Inserts a product review.</summary>
public class InsertProductReviewRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? DataSource { get; set; }
    /// <summary>ProductReview JSON object as documented at https://developers.google.com/merchant/api/reference/rest/reviews_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Deletes a product review.</summary>
public class DeleteProductReviewRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProductReviewId (Required.)</summary>
    [Required] public string ProductReviewId { get; set; } = string.Empty;
}
