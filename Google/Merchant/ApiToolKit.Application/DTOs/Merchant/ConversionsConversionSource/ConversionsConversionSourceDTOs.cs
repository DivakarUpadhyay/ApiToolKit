using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.ConversionsConversionSource;

/// <summary>Creates a new conversion source.</summary>
public class CreateConversionSourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>ConversionSource JSON object as documented at https://developers.google.com/merchant/api/reference/rest/conversions_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Updates information of an existing conversion source.</summary>
public class UpdateConversionSourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>ConversionSourceId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ConversionSourceId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>ConversionSource JSON object as documented at https://developers.google.com/merchant/api/reference/rest/conversions_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Archives an existing conversion source.</summary>
public class DeleteConversionSourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>ConversionSourceId (Required.)</summary>
    [Required] public string ConversionSourceId { get; set; } = string.Empty;
}

/// <summary>Re-enables an archived conversion source.</summary>
public class UndeleteConversionSourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>ConversionSourceId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ConversionSourceId { get; set; } = string.Empty;
    /// <summary>UndeleteConversionSourceRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/conversions_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Fetches a conversion source.</summary>
public class GetConversionSourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>ConversionSourceId (Required.)</summary>
    [Required] public string ConversionSourceId { get; set; } = string.Empty;
}

/// <summary>Retrieves the list of conversion sources the caller has access to.</summary>
public class ListConversionSourcesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
    /// <summary>Optional.</summary>
    public bool? ShowDeleted { get; set; }
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
}
