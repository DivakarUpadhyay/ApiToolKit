using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.BusinessProfile.Review;

/// <summary>List reviews for a location with average rating and total count.</summary>
public class ListReviewsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>updateTime desc, rating, rating desc</summary>
    public string? OrderBy { get; set; }
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}

/// <summary>Get one review.</summary>
public class GetReviewRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Review id</summary>
    [Required] public string ReviewId { get; set; } = string.Empty;
}

/// <summary>Create or update the owner reply to a review.</summary>
public class ReplyToReviewRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Review id</summary>
    [Required] public string ReviewId { get; set; } = string.Empty;
    /// <summary>Reply text</summary>
    [Required] public string Comment { get; set; } = string.Empty;
}

/// <summary>Delete the owner reply to a review.</summary>
public class DeleteReviewReplyRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Review id</summary>
    [Required] public string ReviewId { get; set; } = string.Empty;
}

/// <summary>Get reviews for several locations of an account in one call.</summary>
public class BatchGetReviewsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>accounts/{a}/locations/{l} names</summary>
    [Required] public List<string> LocationNames { get; set; } = new();
    /// <summary>Max 50</summary>
    public int? PageSize { get; set; }
    /// <summary>Next page token</summary>
    public string? PageToken { get; set; }
    /// <summary>updateTime desc, rating, rating desc</summary>
    public string? OrderBy { get; set; }
    /// <summary>Skip reviews without text</summary>
    public bool? IgnoreRatingOnlyReviews { get; set; }
}
