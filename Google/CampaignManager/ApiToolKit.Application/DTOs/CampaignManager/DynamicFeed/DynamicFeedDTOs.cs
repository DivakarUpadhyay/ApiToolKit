using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.DynamicFeed;

/// <summary>Gets a dynamic feed by ID.</summary>
public class GetDynamicFeedRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>DynamicFeedId (Required.)</summary>
    [Required] public string DynamicFeedId { get; set; } = string.Empty;
}

/// <summary>Retransforms a dynamic feed.</summary>
public class RetransformDynamicFeedRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>DynamicFeedId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string DynamicFeedId { get; set; } = string.Empty;
}

/// <summary>Inserts a new dynamic feed.</summary>
public class InsertDynamicFeedRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>DynamicFeedsInsertRequest JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Updates a new dynamic feed.</summary>
public class UpdateDynamicFeedRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>DynamicFeed JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}
