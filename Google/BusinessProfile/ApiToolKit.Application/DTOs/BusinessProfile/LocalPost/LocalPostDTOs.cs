using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.BusinessProfile.LocalPost;

/// <summary>List local posts for a location.</summary>
public class ListLocalPostsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}

/// <summary>Publish a local post on a location.</summary>
public class CreateLocalPostRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Post text</summary>
    [Required] public string Summary { get; set; } = string.Empty;
    /// <summary>e.g. en</summary>
    [Required] public string LanguageCode { get; set; } = string.Empty;
    /// <summary>STANDARD (default), EVENT, OFFER, ALERT</summary>
    public string? TopicType { get; set; }
    /// <summary>BOOK, ORDER, SHOP, LEARN_MORE, SIGN_UP, CALL</summary>
    public string? CallToActionType { get; set; }
    /// <summary>Button URL</summary>
    public string? CallToActionUrl { get; set; }
    /// <summary>Public photo URL</summary>
    public string? MediaUrl { get; set; }
    /// <summary>LocalPostEvent object for EVENT/OFFER posts</summary>
    public object? Event { get; set; }
    /// <summary>LocalPostOffer object for OFFER posts</summary>
    public object? Offer { get; set; }
}

/// <summary>Delete a local post.</summary>
public class DeleteLocalPostRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Local post id</summary>
    [Required] public string PostId { get; set; } = string.Empty;
}

/// <summary>Get one local post.</summary>
public class GetLocalPostRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Local post id</summary>
    [Required] public string PostId { get; set; } = string.Empty;
}

/// <summary>Update a local post (fields in UpdateMask).</summary>
public class UpdateLocalPostRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Local post id</summary>
    [Required] public string PostId { get; set; } = string.Empty;
    /// <summary>Comma-separated fields to update</summary>
    [Required] public string UpdateMask { get; set; } = string.Empty;
    /// <summary>Post text</summary>
    public string? Summary { get; set; }
    /// <summary>LocalPostEvent</summary>
    public object? Event { get; set; }
    /// <summary>LocalPostOffer</summary>
    public object? Offer { get; set; }
    /// <summary>CallToAction</summary>
    public object? CallToAction { get; set; }
}

/// <summary>Get insights (views, actions) for local posts.</summary>
public class GetLocalPostInsightsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Full local post names</summary>
    [Required] public List<string> LocalPostNames { get; set; } = new();
    /// <summary>BasicMetricsRequest (metricRequests, timeRange)</summary>
    [Required] public object BasicRequest { get; set; } = new();
}
