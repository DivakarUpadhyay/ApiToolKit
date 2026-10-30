using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.DimensionValue;

/// <summary>Retrieves list of report dimension values for a list of filters.</summary>
public class QueryDimensionValueRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>ProfileId (The Campaign Manager 360 user profile ID.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ProfileId { get; set; } = string.Empty;
    /// <summary>The value of the nextToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>DimensionValueRequest JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}
