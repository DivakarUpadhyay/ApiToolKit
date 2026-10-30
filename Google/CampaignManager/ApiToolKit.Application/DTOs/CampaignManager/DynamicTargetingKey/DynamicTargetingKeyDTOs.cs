using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.DynamicTargetingKey;

/// <summary>Deletes an existing dynamic targeting key.</summary>
public class DeleteDynamicTargetingKeyRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (ID of the object of this dynamic targeting key.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>DynamicTargetingKeyId (ID of the object of this dynamic targeting key.)</summary>
    [Required] public string DynamicTargetingKeyId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    [Required] public string ObjectType { get; set; } = string.Empty;
}

/// <summary>Inserts a new dynamic targeting key.</summary>
public class InsertDynamicTargetingKeyRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>DynamicTargetingKey JSON object as documented at https://developers.google.com/doubleclick-advertisers/rest/v5</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves a list of dynamic targeting keys.</summary>
public class ListDynamicTargetingKeysRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Select only dynamic targeting keys with this object ID.</summary>
    public string? ObjectId { get; set; }
    /// <summary>Select only dynamic targeting keys exactly matching these names.</summary>
    public List<string>? Names { get; set; }
    /// <summary>Select only dynamic targeting keys with this object type.</summary>
    public string? ObjectType { get; set; }
    /// <summary>Select only dynamic targeting keys whose object has this advertiser ID.</summary>
    public string? AdvertiserId { get; set; }
}
