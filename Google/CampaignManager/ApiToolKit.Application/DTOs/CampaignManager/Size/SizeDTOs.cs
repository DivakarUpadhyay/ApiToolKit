using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.Size;

/// <summary>Retrieves a list of sizes, possibly filtered.</summary>
public class ListSizesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Select only sizes with this height.</summary>
    public int? Height { get; set; }
    /// <summary>Select only sizes with these IDs.</summary>
    public List<string>? Ids { get; set; }
    /// <summary>Select only sizes with this width.</summary>
    public int? Width { get; set; }
    /// <summary>Select only IAB standard sizes.</summary>
    public bool? IabStandard { get; set; }
}

/// <summary>Inserts a new size.</summary>
public class InsertSizeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Size JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Gets one size by ID.</summary>
public class GetSizeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>SizeId (User profile ID associated with this request.)</summary>
    [Required] public string SizeId { get; set; } = string.Empty;
}
