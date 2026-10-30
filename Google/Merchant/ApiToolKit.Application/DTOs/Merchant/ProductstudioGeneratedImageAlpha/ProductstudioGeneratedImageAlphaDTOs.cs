using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.ProductstudioGeneratedImageAlpha;

/// <summary>UpscaleProductImage generates a new image where the resolution of the original image is enhanced.</summary>
public class UpscaleProductImageGeneratedImageRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>UpscaleProductImageRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/productstudio_v1alpha</summary>
    public object? Body { get; set; }
}

/// <summary>RemoveProductImageBackground generates a new image where the background of the original image is removed.</summary>
public class RemoveProductImageBackgroundGeneratedImageRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>RemoveProductImageBackgroundRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/productstudio_v1alpha</summary>
    public object? Body { get; set; }
}

/// <summary>GenerateProductImageBackground generates a new image where the background of the original image is replaced by an AI generated scene based on provided product information and a ...</summary>
public class GenerateProductImageBackgroundGeneratedImageRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>GenerateProductImageBackgroundRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/productstudio_v1alpha</summary>
    public object? Body { get; set; }
}
